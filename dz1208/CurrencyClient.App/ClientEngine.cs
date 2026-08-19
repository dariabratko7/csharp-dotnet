using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyClient.App
{
    public class ClientEngine : IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;

        public bool IsConnected => _client?.Connected ?? false;

        public async Task ConnectAsync(string host, int port)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(host, port);

            _stream = _client.GetStream();
            _reader = new StreamReader(_stream, Encoding.UTF8);
            _writer = new StreamWriter(_stream, Encoding.UTF8) { AutoFlush = true };
        }

        public async Task<string> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Немає підключення до сервера.");

            string request = $"{fromCurrency} {toCurrency}";
            await _writer.WriteLineAsync(request);

            string response = await _reader.ReadLineAsync();
            return response;
        }

        public void Disconnect()
        {
            try
            {
                if (IsConnected)
                    _writer?.WriteLine("EXIT");
            }
            catch { }
            finally
            {
                _reader?.Dispose();
                _writer?.Dispose();
                _stream?.Dispose();
                _client?.Close();
            }
        }

        public void Dispose() => Disconnect();
    }
}