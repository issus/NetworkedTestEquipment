using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCircuit.Electronics.TestEquipment.LCRMeter.RohdeLCX.Commands
{
    /// <summary>
    /// Represents the Function Subsystem of the test equipment.
    /// </summary>
    public class FunctionSubsystem
    {
        NetworkTestInstrument equipment;

        /// <summary>
        /// Initializes a new instance of the FunctionSubsystem class with a specified test equipment object.
        /// </summary>
        /// <param name="equipment">The test equipment object to use for communication.</param>
        public FunctionSubsystem(NetworkTestInstrument equipment)
        {
            this.equipment = equipment;
        }

        /// <summary>
        /// Output impedance for the measurement.
        /// </summary>
        public enum ImpedanceSource
        {
            /// <summary>
            /// 10 Ohms Output Impedance
            /// </summary>
            LOW,
            /// <summary>
            /// 100 Ohms Output Impedance
            /// </summary>
            HIGH,
        }

        /// <summary>
        /// Enum representing different types of impedance measurements on an LCR meter.
        /// </summary>
        public enum FunctionImpedance
        {
            /// <summary>
            /// Parallel capacitance, dissipation factor
            /// </summary>
            CPD,
            /// <summary>
            /// Parallel capacitance, quality factor
            /// </summary>
            CPQ,
            /// <summary>
            /// Parallel capacitance, conductance
            /// </summary>
            CPG,
            /// <summary>
            /// Parallel capacitance, parallel resistance
            /// </summary>
            CPRP,
            /// <summary>
            /// Serial capacitance, conductance
            /// </summary>
            CSD,
            /// <summary>
            /// Serial capacitance, quality factor
            /// </summary>
            CSQ,
            /// <summary>
            /// Serial capacitance, serial resistance
            /// </summary>
            CSRS,
            /// <summary>
            /// Parallel inductance, dissipation factor
            /// </summary>
            LPD,
            /// <summary>
            /// Parallel inductance, quality factor
            /// </summary>
            LPQ,
            /// <summary>
            /// Parallel inductance, conductance
            /// </summary>
            LPG,
            /// <summary>
            /// Parallel inductance, parallel resistance
            /// </summary>
            LPRP,
            /// <summary>
            /// Parallel inductance, direct current resistance
            /// </summary>
            LPRDc,
            /// <summary>
            /// Serial inductance, conductance
            /// </summary>
            LSD,
            /// <summary>
            /// Serial inductance, quality factor
            /// </summary>
            LSQ,
            /// <summary>
            /// Serial inductance, serial resistance
            /// </summary>
            LSRS,
            /// <summary>
            /// Resistance, Impedance
            /// </summary>
            RX,
            /// <summary>
            /// Parallel resistance, susceptance
            /// </summary>
            RPB,
            /// <summary>
            /// Direct current resistance
            /// </summary>
            RDC,
            /// <summary>
            /// Mutual inductance, Phase angle degree
            /// </summary>
            MTD,
            /// <summary>
            /// Transformer ratio, Phase angle degree
            /// </summary>
            NTD,
            /// <summary>
            /// Impedance, Phase angle degree
            /// </summary>
            ZTD,
            /// <summary>
            /// Impedance, phase angle radians
            /// </summary>
            ZTR,
            /// <summary>
            /// Conductance, susceptance
            /// </summary>
            GB,
            /// <summary>
            /// Admittance, phase angle degree
            /// </summary>
            YTD,
            /// <summary>
            /// Admittance, phase angle radians
            /// </summary>
            YTR
        }

        /// <summary>
        /// Selects the measurement function.
        /// </summary>
        public enum MeasurementFunction
        {
            /// <summary>
            /// Impedance measurement.
            /// </summary>
            L,
            /// <summary>
            /// Capacitance measurement.
            /// </summary>
            C,
            /// <summary>
            /// Resistance measurement.
            /// </summary>
            R,
            /// <summary>
            /// Transformer measurement.
            /// </summary>
            T

        }

        /// <summary>
        /// Selects the impedance range for transformer measurement.
        /// </summary>
        public enum TransformerRange
        {
            /// <summary>
            /// 50 Turn Ratio
            /// </summary>
            N50,
            /// <summary>
            /// 500 Turn Ratio
            /// </summary>
            N500

        }

        /// <summary>
        /// Activates automatic impedance range selection.
        /// </summary>
        /// <returns></returns>
        public async Task SetImpedanceAutoRange(bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:IMP:RANG:AUTO {(state ? '1' : '0')}");
        }

        /// <summary>
        /// Freezes the set impedance measurement range.
        /// </summary>
        /// <returns></returns>
        public async Task SetImpedanceRangeHold(bool state)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:IMP:RANG:HOLD {(state ? '1' : '0')}");
        }

        /// <summary>
        /// Sets the impedance range value. For setting the parameter manually, disable auto selection with <see cref="SetImpedanceAutoRange"/>
        /// </summary>
        /// <returns></returns>
        public async Task SetImpedanceRangeValue(int range)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:IMP:RANG:VAL {range}");
        }

        /// <summary>
        /// Selects the output impedance for the measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetImpedanceSource(ImpedanceSource status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:IMP:SOUR {status}");
        }

        /// <summary>
        /// Queries which output impedance is selected
        /// </summary>
        /// <returns></returns>
        public async Task<ImpedanceSource> QueryImpedanceSource()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FUNC:IMP:SOUR?");
            return Enum.Parse<ImpedanceSource>(await equipment.ReadString());
        }

        /// <summary>
        /// Selects the impedance parameter for the measurement corresponding to the measurement type.
        /// </summary>
        /// <returns></returns>
        public async Task SetFunctionImpedance(FunctionImpedance status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:IMP:TYPE {status}");
        }

        /// <summary>
        /// Queries the impedance parameter for the measurement corresponding to the measurement type.
        /// </summary>
        /// <returns></returns>
        public async Task<FunctionImpedance> QueryFunctionImpedance()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FUNC:IMP:TYPE?");
            return Enum.Parse<FunctionImpedance>(await equipment.ReadString());
        }

        /// <summary>
        /// Selects the measurement function.
        /// </summary>
        /// <returns></returns>
        public async Task SetMeasurementFunction(MeasurementFunction status)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:MEAS:TYPE {status}");
        }

        /// <summary>
        /// Queries the measurement function.
        /// </summary>
        /// <returns></returns>
        public async Task<MeasurementFunction> QueryMeasurementFunction()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FUNC:MEAS:TYPE?");
            return Enum.Parse<MeasurementFunction>(await equipment.ReadString());
        }

        /// <summary>
        /// Selects the impedance range for transformer measurement.
        /// </summary>
        /// <returns></returns>
        public async Task SetFunctionTransformerRange(TransformerRange TurnRatio)
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.SendCommand($"FUNC:MEAS:TYPE {TurnRatio}");
        }

        /// <summary>
        /// Queries the impedance range for transformer measurement.
        /// </summary>
        /// <returns></returns>
        public async Task<TransformerRange> QueryFunctionTransformerRange()
        {
            if (equipment is null || !equipment.IsConnected)
                throw new Exception("Test equipment not connected");

            await equipment.ClearBuffer();

            await equipment.SendCommand($"FUNC:MEAS:TYPE?");
            return Enum.Parse<TransformerRange>(await equipment.ReadString());
        }
    }
}