using Gx.Rs.Api.Util;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp.Portable;

namespace Gx.Rs.Api
{
    /// <summary>
    /// Represents an exception within the FamilySearch GEDCOM X application.
    /// </summary>
    public class GedcomxApplicationException : Exception
    {
        /// <summary>
        /// Gets the response associated with the exception if applicable.
        /// </summary>
        /// <value>
        /// The response associated with the exception if applicable.
        /// </value>
        public IRestResponse Response { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        public GedcomxApplicationException()
            : this(null as string)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GedcomxApplicationException(string message)
            : this(message, null as Exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public GedcomxApplicationException(string message, Exception innerException)
            : this(message, innerException, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        /// <param name="response">The REST API response that is associated with this exception.</param>
        public GedcomxApplicationException(IRestResponse response)
            : this(null as string, response)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="response">The REST API response that is associated with this exception.</param>
        public GedcomxApplicationException(string message, IRestResponse response)
            : this(message, null, response)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GedcomxApplicationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="response">The REST API response that is associated with this exception.</param>
        public GedcomxApplicationException(string message, Exception innerException, IRestResponse response)
            : base(message, innerException)
        {
            Response = response;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                String message = base.Message;
                StringBuilder builder = new StringBuilder(message == null ? "Error processing GEDCOM X request." : message);
                List<HttpWarning> warnings = Warnings;
                if (message != null || warnings.Count > 0)
                {
                    if (warnings != null)
                    {
                        foreach (HttpWarning warning in warnings)
                        {
                            builder.Append("\nWarning: ").Append(warning.Message);
                        }
                    }
                }

                String body = null;
                if (this.Response != null)
                {
                    try
                    {
                        body = Encoding.UTF8.GetString(this.Response.RawBytes, 0, this.Response.RawBytes.Length);
                    }
                    catch (Exception)
                    {
                        //unable to get the response body...
                        body = "(error response body unavailable)";
                    }
                }
                if (body != null)
                {
                    builder.Append('\n').Append(body);
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Gets the list of warning header values from the associated REST API response if it is available.
        /// </summary>
        /// <value>
        /// The list of warning header values from the associated REST API response if it is available.
        /// </value>
        public List<HttpWarning> Warnings
        {
            get
            {
                List<HttpWarning> warnings = null;

                if (this.Response != null)
                {
                    var values = this.Response.Headers.GetValuesSafe("Warning");
                    if (values != null && values.Any())
                    {
                        warnings = new List<HttpWarning>();
                        foreach (var value in values)
                        {
                            warnings.Add(HttpWarning.Parse(value));
                        }
                    }
                }

                return warnings;
            }
        }
    }
}
