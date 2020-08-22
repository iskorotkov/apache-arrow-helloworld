using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Apache.Arrow;
using Apache.Arrow.Ipc;
using Apache.Arrow.Memory;

namespace ApacheArrowCs
{
    internal static class Program
    {
        private static async Task Main()
        {
            await Write();
            await Read();
        }

        private static async Task Write()
        {
            var memoryAllocator = new NativeMemoryAllocator();
            var recordBatch = new RecordBatch.Builder(memoryAllocator)
                .Append("Column A", false, col => col.Int32(array =>
                    array.AppendRange(Enumerable.Range(0, 10))))
                .Append("Column B", false,
                    col => col.Float(array =>
                        array.AppendRange(Enumerable.Range(0, 10).Select(x => Convert.ToSingle(x * 2)))))
                .Append("Column C", false, col => col.String(array =>
                    array.AppendRange(Enumerable.Range(0, 10).Select(x => $"Item {x + 1}"))))
                .Append("Column D", false, col => col.Boolean(array =>
                    array.AppendRange(Enumerable.Range(0, 10).Select(x => x % 2 == 0))))
                .Build();

            Console.WriteLine($"Allocations: {memoryAllocator.Statistics.Allocations}");
            Console.WriteLine($"Allocated: {memoryAllocator.Statistics.BytesAllocated} byte(s)");

            await using var stream = File.OpenWrite("test.arrow");
            using var writer = new ArrowFileWriter(stream, recordBatch.Schema);

            await writer.WriteRecordBatchAsync(recordBatch);
            await writer.WriteEndAsync();
        }

        private static async Task Read()
        {
            await using var stream = File.OpenRead("test.arrow");
            using var reader = new ArrowFileReader(stream);

            var batches = await reader.RecordBatchCountAsync();
            for (var i = 0; i < batches; i++)
            {
                var recordBatch = await reader.ReadNextRecordBatchAsync();
                Console.WriteLine($"Columns: {recordBatch.ColumnCount}");
            }
        }
    }
}