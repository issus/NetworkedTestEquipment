using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.PowerSupply.RigolDP800.Commands
{
    /// <summary>
    /// Provides access to the status register of a Rigol DP800 series power supply instrument.
    /// </summary>
    public class Status
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the Status class with the specified equipment object.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument object representing the connected DP800 series power supply instrument.</param>
        public Status(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the condition register of the questionable status register
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionableStatus> QueryCondition()
        {
            await equipment.ClearBuffer();

            await equipment.SendCommand("STAT:QUES:COND?");

            int response = await equipment.ReadInt();
            return (QuestionableStatus)response;
        }

        /// <summary>
        /// Enables bits in the enable register part of the questionable status register
        /// </summary>
        /// <param name="enable">Events to enable</param>
        /// <returns></returns>
        public async Task SetEnableRegister(QuestionableStatus enable)
        {
            await equipment.SendCommand($"STAT:QUES:ENAB {(int)enable}");
        }

        /// <summary>
        /// Queries the bit enabled in the enable register part of the
        /// questionable status register
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionableStatus> QueryEnableRegister()
        {
            await equipment.ClearBuffer();

            await equipment.SendCommand("STAT:QUES:ENAB?");

            int response = await equipment.ReadInt();
            return (QuestionableStatus)response;
        }

        /// <summary>
        /// Queries the event register of the questionable status register
        /// </summary>
        /// <returns></returns>
        public async Task<QuestionableStatus> QueryEventStatus()
        {
            await equipment.ClearBuffer();

            await equipment.SendCommand("STAT:QUES?");

            int response = await equipment.ReadInt();
            return (QuestionableStatus)response;
        }

        /// <summary>
        /// Clears all the bits in the event register part of the questionable status
        /// register
        /// </summary>
        /// <returns></returns>
        public async Task ClearEventRegister()
        {
            await equipment.SendCommand("STAT:PRES");
        }
    }
}
