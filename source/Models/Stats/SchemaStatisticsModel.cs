using System;

namespace Models.Stats
{
    /// <summary>
    /// Represents a model for storing statistics related to a schema.
    /// </summary>
    public class SchemaStatisticsModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the schema statistics.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the schema.
        /// </summary>
        public string SchemaFullName { get; set; }

        /// <summary>
        /// Gets or sets the number of instances of the schema.
        /// </summary>
        public int InstanceCount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the schema statistics were last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the last instance of the schema was created.
        /// </summary>
        public DateTime LastInstanceCreated { get; set; }
    }
}
