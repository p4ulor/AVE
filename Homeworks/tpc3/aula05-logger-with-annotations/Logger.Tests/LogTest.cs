using System;
using Xunit;
using Logger;
using System.Text;

namespace Logger.Tests
{

    class BufferPrinter : IPrinter
    {
        public StringBuilder buffer = new StringBuilder();
        public void Print(string output)
        {
            buffer.Append(output);
        }
    }

    public class LogTest
    {
        [Fact]
        public void TestLogInfo()
        {
            // Arrange (setup)
            Point p = new Point(7,9);
            BufferPrinter printer = new BufferPrinter();
            Log log = new Log(printer); //(log writes to the printer)

            // Act
            log.Info(p);

            // Assert
            Assert.Equal(
                "GetModule: 11,40175425099138, x: 7, ",
                printer.buffer.ToString()
            );
        }
    }
}
