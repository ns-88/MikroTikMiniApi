using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public class Interface : ModelBase, IModelFactory<Interface>
    {
        public string? Name { get; private set; }
        public string? DefaultName { get; private set; }
        public string? Type { get; private set; }
        public string? Mtu { get; private set; }
        public int? ActualMtu { get; private set; }
        public int? L2Mtu { get; private set; }
        public int? MaxL2Mtu { get; private set; }
        public string? MacAddress { get; private set; }
        public string? LastLinkDownTime { get; private set; }
        public string? LastLinkUpTime { get; private set; }
        public int? LinkDowns { get; private set; }
        public ulong? RxByte { get; private set; }
        public ulong? TxByte { get; private set; }
        public ulong? RxPacket { get; private set; }
        public ulong? TxPacket { get; private set; }
        public ulong? RxDrop { get; private set; }
        public ulong? TxDrop { get; private set; }
        public ulong? TxQueueDrop { get; private set; }
        public ulong? RxError { get; private set; }
        public ulong? TxError { get; private set; }
        public ulong? FpRxByte { get; private set; }
        public ulong? FpTxByte { get; private set; }
        public ulong? FpRxPacket { get; private set; }
        public ulong? FpTxPacket { get; private set; }
        public bool? IsRunning { get; private set; }
        public bool? IsDisabled { get; private set; }

        Interface IModelFactory<Interface>.Create(IApiSentence sentence)
        {
            return new Interface
            {
                Id = GetStringValueOrDefault(".id", sentence),
                Name = GetStringValueOrDefault("name", sentence),
                DefaultName = GetStringValueOrDefault("default-name", sentence),
                Type = GetStringValueOrDefault("type", sentence),
                Mtu = GetStringValueOrDefault("mtu", sentence),
                ActualMtu = GetIntValueOrDefault("actual-mtu", sentence),
                L2Mtu = GetIntValueOrDefault("l2mtu", sentence),
                MaxL2Mtu = GetIntValueOrDefault("max-l2mtu", sentence),
                MacAddress = GetStringValueOrDefault("mac-address", sentence),
                LastLinkDownTime = GetStringValueOrDefault("last-link-down-time", sentence),
                LastLinkUpTime = GetStringValueOrDefault("last-link-up-time", sentence),
                LinkDowns = GetIntValueOrDefault("link-downs", sentence),
                RxByte = GetUlongValueOrDefault("rx-byte", sentence),
                TxByte = GetUlongValueOrDefault("tx-byte", sentence),
                RxPacket = GetUlongValueOrDefault("rx-packet", sentence),
                TxPacket = GetUlongValueOrDefault("tx-packet", sentence),
                RxDrop = GetUlongValueOrDefault("rx-drop", sentence),
                TxDrop = GetUlongValueOrDefault("tx-drop", sentence),
                TxQueueDrop = GetUlongValueOrDefault("tx-queue-drop", sentence),
                RxError = GetUlongValueOrDefault("rx-error", sentence),
                TxError = GetUlongValueOrDefault("tx-error", sentence),
                FpRxByte = GetUlongValueOrDefault("fp-rx-byte", sentence),
                FpTxByte = GetUlongValueOrDefault("fp-tx-byte", sentence),
                FpRxPacket = GetUlongValueOrDefault("fp-rx-packet", sentence),
                FpTxPacket = GetUlongValueOrDefault("fp-tx-packet", sentence),
                IsRunning = GetBoolValueOrDefault("running", sentence),
                IsDisabled = GetBoolValueOrDefault("disabled", sentence)
            };
        }
    }
}