using System;

namespace DABTechs.eCommerce.Sales.Domain
{
    /// <summary>
    /// The VIP Sale Template Page.
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the template body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Template"/> class.
        /// </summary>
        public Template()
        {
            LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Gets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public double Age
        {
            get
            {
                return (DateTime.Now - LastUpdated).TotalMinutes;
            }
        }

        /// <summary>
        /// Replaces the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="message">The message.</param>
        public void Replace(string key, string message)
        {
            Body = Body.Replace(key, message);
        }
    }
}