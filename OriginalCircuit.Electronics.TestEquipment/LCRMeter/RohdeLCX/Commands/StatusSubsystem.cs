using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents the status subsystem of the Rohde LCX200 instrument.
    /// </summary>
    public class StatusSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the StatusSubsystem class with the specified NetworkTestInstrument.
        /// </summary>
        /// <param name="equipment">The NetworkTestInstrument used to communicate with the instrument.</param>
        public StatusSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Queries the CONDition part of the operational status register.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryStatusOperationRegisterStatus()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:OPER:COND?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Enables the bits in the enable register for the Standard Operation Register group.
        /// </summary>
        /// <returns></returns>
        public async Task SetEnableStatusOperationRegister(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:OPER:ENAB {value}");
        }


        /// <summary>
        /// Queries the bits in the enable register for the Standard Operation Register group.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryEnableStatusOperationRegister()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:OPER:ENAB?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Sets the negative transition filter to operation register.
        /// </summary>
        /// <returns></returns>
        public async Task SetNegativeTranslationFilter(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:OPER:NTR {value}");
        }


        /// <summary>
        /// Queries the negative transition filter to operation register.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryNegativeTranslationFilter()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:OPER:NTR?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Sets the positive transition filter to operation register.
        /// </summary>
        /// <returns></returns>
        public async Task SetPositiveTranslationFilter(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:OPER:PTR {value}");
        }


        /// <summary>
        /// Queries the positive transition filter to operation register.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryPositiveTranslationFilter()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:OPER:PTR?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Queries the actions the instrument has executed since the last reading. (Operation register)
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryLastInstrumentAction()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:OPER:EVEN?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Returns the contents of the CONDition part of the status register to check for questionable instrument or measurement states.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryQuestionableRegisterCondition()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:QUES:COND?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Sets the enable mask that allows true conditions in the EVENt part to be reported in the summary bit.
        /// </summary>
        /// <returns></returns>
        public async Task SetEnableStatusQuestionableRegister(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:QUES:ENAB {value}");
        }


        /// <summary>
        /// Queries the enable register and returns a decimal value which corresponds to the binary-weighted sum.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryEnableStatusQuestionableRegister()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:QUES:ENAB?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Sets the negative transition filter to questionable register.
        /// </summary>
        /// <returns></returns>
        public async Task SetNegativeTranslationFilter2(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:QUES:NTR {value}");
        }


        /// <summary>
        /// Queries the negative transition filter to questionable register.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryNegativeTranslationFilter2()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:QUES:NTR?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Sets the positive transition filter to questionable register.
        /// </summary>
        /// <returns></returns>
        public async Task SetPositiveTranslationFilter2(int value)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"STAT:QUES:PTR {value}");
        }


        /// <summary>
        /// Queries the positive transition filter to questionable register.
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryPositiveTranslationFilter2()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:QUES:PTR?");
            return await equipment.ReadInt();
        }


        /// <summary>
        /// Queries the actions the instrument has executed since the last reading. (Questionable register)
        /// </summary>
        /// <returns></returns>
        public async Task<int> QueryLastInstrumentAction2()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"STAT:QUES:EVEN?");
            return await equipment.ReadInt();
        }
    }
}