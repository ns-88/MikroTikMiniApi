using MikroTikMiniApi.Interfaces.Factories;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Models.Api
{
    public class Interface : ModelBase, IModelFactory<Interface>
    {
        public string Name { get; private set; }
        public string DefaultName { get; private set; }
        public string Type { get; private set; }
        public string Mtu { get; private set; }
        public int? ActualMtu { get; private set; }
        public int? L2Mtu { get; private set; }
        public int? MaxL2Mtu { get; private set; }
        public string MacAddress { get; private set; }
        public string LastLinkDownTime { get; private set; }
        public string LastLinkUpTime { get; private set; }
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
                Id = GetStringValue(".id", sentence),
                Name = GetStringValue("name", sentence),
                DefaultName = GetStringValue("default-name", sentence),
                Type = GetStringValue("type", sentence),
                Mtu = GetStringValue("mtu", sentence),
                ActualMtu = GetIntValue("actual-mtu", sentence),
                L2Mtu = GetIntValue("l2mtu", sentence),
                MaxL2Mtu = GetIntValue("max-l2mtu", sentence),
                MacAddress = GetStringValue("mac-address", sentence),
                LastLinkDownTime = GetStringValue("last-link-down-time", sentence),
                LastLinkUpTime = GetStringValue("last-link-up-time", sentence),
                LinkDowns = GetIntValue("link-downs", sentence),
                RxByte = GetUlongValue("rx-byte", sentence),
                TxByte = GetUlongValue("tx-byte", sentence),
                RxPacket = GetUlongValue("rx-packet", sentence),
                TxPacket = GetUlongValue("tx-packet", sentence),
                RxDrop = GetUlongValue("rx-drop", sentence),
                TxDrop = GetUlongValue("tx-drop", sentence),
                TxQueueDrop = GetUlongValue("tx-queue-drop", sentence),
                RxError = GetUlongValue("rx-error", sentence),
                TxError = GetUlongValue("tx-error", sentence),
                FpRxByte = GetUlongValue("fp-rx-byte", sentence),
                FpTxByte = GetUlongValue("fp-tx-byte", sentence),
                FpRxPacket = GetUlongValue("fp-rx-packet", sentence),
                FpTxPacket = GetUlongValue("fp-tx-packet", sentence),
                IsRunning = GetBoolValue("running", sentence),
                IsDisabled = GetBoolValue("disabled", sentence)
            };
        }
    }
}