using System;

namespace EnumGenie.Core.Writers
{
    public static class WriterExtensions
    {
        /// <summary>
        /// Output the result to a single file
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="file">Path to the file to output.</param>
        /// <param name="configure">Configure the writer for this output</param>
        public static EnumGenieGenerator File(this Writer writer, string file, Action<WriterConfig> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var config = new WriterConfig();
            configure(config);

            return writer.Custom(new FileWriter(file, config.Writer));
        }

        /// <summary>
        /// Output the result to the console
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="configure">Configure the writer for this output</param>
        public static EnumGenieGenerator Console(this Writer writer, Action<WriterConfig> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var config = new WriterConfig();
            configure(config);

            return writer.Custom(new ConsoleWriter(config.Writer));
        }
    }
}