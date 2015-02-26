using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace Gx.Rs.Api.Util
{
    /// <summary>
    /// Provide a simple implementation of <see cref="IDataSource"/> for files.
    /// </summary>
    public class FileDataSource : IDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDataSource"/> class.
        /// </summary>
        public FileDataSource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDataSource"/> class.
        /// </summary>
        /// <param name="file">The file to be represented by this instance.</param>
        public FileDataSource(String file)
        {
            var mime = new MimeSharp.Mime();
            ContentType = mime.Lookup(file);

            var fileWrapper = FileSystem.Current.GetFileFromPathAsync(file).Result;
            InputStream = fileWrapper.OpenAsync(FileAccess.Read).Result;
            Name = Path.GetFileName(file);
        }

        /// <summary>
        /// Gets the input stream of the file specified during instantiation.
        /// </summary>
        /// <value>
        /// The input stream of the file specified during instantiation.
        /// </value>
        public Stream InputStream
        {
            get;
            private set;
        }

        /// <summary>
        /// Content-type is the Apache standard for the given file extension.
        /// </summary>
        public String ContentType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the data source.
        /// </summary>
        /// <value>
        /// The name of the data source.
        /// </value>
        public String Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (InputStream != null)
            {
                InputStream.Dispose();
            }
        }
    }
}
