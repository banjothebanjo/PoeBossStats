﻿//-----------------------------------------------------------------------
// <copyright file="ClientLurker.cs" company="Wohs Inc.">
//     Copyright © Wohs Inc.
// </copyright>
//-----------------------------------------------------------------------

namespace Lurker
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Lurker.Extensions;
    using Lurker.Patreon.Events;
    using Sentry;

    /// <summary>
    /// Defines a file Watcher for the Client log file.
    /// </summary>
    public class ClientLurker : IDisposable
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly string ClientLogFileName = "Client.txt";
        private static readonly string ClientLogFolderName = "logs";

        public string _lastLine;
        public string _instanceIp;
        private CancellationTokenSource _tokenSource;
        private Process _pathOfExileProcess;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLurker" /> class.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        public ClientLurker(int processId)
        {
            this._pathOfExileProcess = ProcessLurker.GetProcessById(processId);
            if (this._pathOfExileProcess != null)
            {
                this._tokenSource = new CancellationTokenSource();

                if (!this._pathOfExileProcess.ProcessName.EndsWith("_KG.exe"))
                {
                    this.Lurk();
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        private string FilePath { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [request admin].
        /// </summary>
        public event EventHandler AdminRequested;

        /// <summary>
        /// Occurs when the player changed the location.
        /// </summary>
        public event EventHandler<LocationChangedEvent> LocationChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._tokenSource.Cancel();
            }
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The current line of the stream position.
        /// </returns>
        /// <exception cref="IOException">Error reading from file.</exception>
        private static string GetLine(Stream stream)
        {
            // while we have not yet reached start of file, read bytes backwards until '\n' byte is hit
            var lineLength = 0;
            while (stream.Position > 0)
            {
                stream.Position--;
                var byteFromFile = stream.ReadByte();

                if (byteFromFile < 0)
                {
                    // the only way this should happen is if someone truncates the file out from underneath us while we are reading backwards
                    throw new IOException("Error reading from file");
                }
                else if (byteFromFile == '\n')
                {
                    // we found the new line, break out, fs.Position is one after the '\n' char
                    break;
                }

                lineLength++;
                stream.Position--;
            }

            var oldPosition = stream.Position;
            var bytes = new BinaryReader(stream).ReadBytes(lineLength - 1);

            // -1 is the \n
            stream.Position = oldPosition - 1;
            return Encoding.UTF8.GetString(bytes).Replace(System.Environment.NewLine, string.Empty);
        }

        /// <summary>
        /// Reads the last line from ut f8 encoded file.
        /// </summary>
        /// <returns>The last line.</returns>
        /// <exception cref="System.IO.IOException">Error reading from file at " + path.</exception>
        private string GetLastLine()
        {
            using (var stream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length == 0)
                {
                    return null;
                }

                stream.Position = stream.Length - 1;

                return GetLine(stream);
            }
        }

        /// <summary>
        /// Gets the new lines.
        /// </summary>
        /// <returns>The all the new lines.</returns>
        private IEnumerable<string> GetNewLines()
        {
            using (var stream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length == 0)
                {
                    return null;
                }

                stream.Position = stream.Length - 1;

                var newLines = new List<string>();
                var currentNewLine = GetLine(stream);
                while (this._lastLine != currentNewLine)
                {
                    newLines.Add(currentNewLine);
                    currentNewLine = GetLine(stream);
                }

                return newLines;
            }
        }

        /// <summary>
        /// Lurks this instance.
        /// </summary>
        public void Lurk()
        {
            string path = string.Empty;
            try
            {
                path = this._pathOfExileProcess.GetMainModuleFileName();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                this.AdminRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            this.FilePath = Path.Combine(Path.GetDirectoryName(path), ClientLogFolderName, ClientLogFileName);
            this._lastLine = this.GetLastLine();
            this.LurkLastLine();
        }

        /// <summary>
        /// Lurks this instance.
        /// </summary>
        private async void LurkLastWriteTime()
        {
            Logger.Trace("Lurk with last write time");
            var fileInformation = new FileInfo(this.FilePath);
            var lastWriteTime = fileInformation.LastWriteTimeUtc;

            var token = this._tokenSource.Token;
            while (true)
            {
                do
                {
                    await Task.Delay(500);
                    fileInformation.Refresh();

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                }
                while (fileInformation.LastWriteTimeUtc == lastWriteTime);

                lastWriteTime = fileInformation.LastAccessTimeUtc;
                this.OnFileChanged(this.GetLastLine());
            }
        }

        /// <summary>
        /// Lurks the last line.
        /// </summary>
        private async void LurkLastLine()
        {
            Logger.Trace("Lurk with last line");

            var token = this._tokenSource.Token;
            while (true)
            {
                IEnumerable<string> newLines;
                do
                {
                    await Task.Delay(500);
                    newLines = this.GetNewLines();
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                }
                while (newLines.Count() == 0);

                this._lastLine = newLines.First();
                foreach (var line in newLines)
                {
                    LocationIpAddress(line);
                    this.OnFileChanged(line);
                }

                if (this._lastLine.Contains("Celestial Hideout"))
                {
                    Console.WriteLine(this._lastLine);
                }
            }
        }


        private void LocationIpAddress(string line)
        {
            var stringArray = line.Split(' ');
            foreach (var item in stringArray)
            {
                var isIp = Regex.Match(item, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}$");
                if (isIp.Success)
                {
                    this._instanceIp = isIp.Value;
                }
            }
        }

        /// <summary>
        /// Called when [file changed].
        /// </summary>
        /// <param name="newline">The newline.</param>
        private void OnFileChanged(string newline)
        {
            if (string.IsNullOrEmpty(newline))
            {
                return;
            }

            try
            {
                var locationEvent = LocationChangedEvent.TryParse(newline);
                if (locationEvent != null)
                {
                    this.LocationChanged?.Invoke(this, locationEvent);
                    return;
                }

                Logger.Trace($"Not parsed: {newline}");
            }
            catch (Exception ex)
            {
                var lineError = $"Line in error: {newline}";
                var exception = new Exception(lineError, ex);
                Logger.Error(exception, exception.Message);

#if !DEBUG
                SentrySdk.AddBreadcrumb(message: lineError, level: Sentry.Protocol.BreadcrumbLevel.Error);
                SentrySdk.CaptureException(ex);
#endif
            }
        }

        /// <summary>
        /// Tests the file last write time.
        /// </summary>
        /// <returns>True if enable.</returns>
        private bool FileLastWriteTimeEnable()
        {
            var filePath = Path.GetTempFileName();
            var fileInformation = new FileInfo(filePath);
            var initialeWriteTime = fileInformation.LastWriteTimeUtc;

            Thread.Sleep(500);
            File.WriteAllText(filePath, "TestWriteTime");
            fileInformation.Refresh();
            File.Delete(filePath);

            return initialeWriteTime != fileInformation.LastWriteTimeUtc;
        }

        #endregion
    }
}