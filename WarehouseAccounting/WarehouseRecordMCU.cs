using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseAccounting
{
    public class WarehouseRecordMCU : WarehouseRecord
    {
        public enum TypeMCU
        {
            ATMEGA328PU,
            ATMEGA16PU,
            ATMEGA32PU,
            ATMEGA64PU,
            ATTINY13A,
            ATTINY24A
        }

        public float CPU_FREQUENCY { get; }
        public float SRAM          { get; }
        public float FLASH         { get; }
        public float EEPROM        { get; }
        public int   GPIO          { get; }
        public int   PWM_CHANNELS  { get; }

        public WorkingConditions workingConditions;

        public WarehouseRecordMCU(
            short id, string name, short count, Location location,
            float CPU_FREQUENCY, float SRAM, float FLASH, float EEPROM, int GPIO, int PWM_CHANNELS,
            WorkingConditions workingConditions) : base(id, name, count, location)
        {
            this.CPU_FREQUENCY     = CPU_FREQUENCY;
            this.SRAM              = SRAM;
            this.FLASH             = FLASH;
            this.EEPROM            = EEPROM;
            this.GPIO              = GPIO;
            this.PWM_CHANNELS      = PWM_CHANNELS;
            this.workingConditions = workingConditions;
        }

        public WarehouseRecordMCU(
            short id, string name, short count, Location location,
            float CPU_FREQUENCY, float SRAM, float FLASH, float EEPROM, int GPIO, int PWM_CHANNELS,
            float temperatureMin, float temperatureMax, float voltageMin, float voltageMax) : base(id, name, count, location)
        {
            this.CPU_FREQUENCY     = CPU_FREQUENCY;
            this.SRAM              = SRAM;
            this.FLASH             = FLASH;
            this.EEPROM            = EEPROM;
            this.GPIO              = GPIO;
            this.PWM_CHANNELS      = PWM_CHANNELS;
            this.workingConditions = new WorkingConditions(
                    new WorkingConditions.RangeOfValues(temperatureMin, temperatureMax),
                    new WorkingConditions.RangeOfValues(voltageMin, voltageMax)
                );
        }

        public static explicit operator WarehouseRecordGeneral(WarehouseRecordMCU warehouseRecordMCU)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartObject();

                    writer.WriteNumber("CPU_FREQUENCY", warehouseRecordMCU.CPU_FREQUENCY);
                    writer.WriteNumber("PWM_CHANNELS",  warehouseRecordMCU.PWM_CHANNELS);
                    writer.WriteNumber("GPIO",          warehouseRecordMCU.GPIO);
                    writer.WriteNumber("SRAM",          warehouseRecordMCU.SRAM);
                    writer.WriteNumber("FLASH",         warehouseRecordMCU.FLASH);
                    writer.WriteNumber("EEPROM",        warehouseRecordMCU.EEPROM);

                    writer.WriteStartObject("VOLTAGE");
                    writer.WriteNumber("MAX", warehouseRecordMCU.workingConditions.voltage.max);
                    writer.WriteNumber("MIN", warehouseRecordMCU.workingConditions.voltage.min);
                    writer.WriteEndObject();

                    writer.WriteStartObject("TEMPERATURE");
                    writer.WriteNumber("MAX", warehouseRecordMCU.workingConditions.temperature.max);
                    writer.WriteNumber("MIN", warehouseRecordMCU.workingConditions.temperature.min);
                    writer.WriteEndObject();

                    writer.WriteEndObject();
                }

                stream.Seek(0, SeekOrigin.Begin);
                JsonDocument jsonDocument = JsonDocument.Parse(stream);

                return new WarehouseRecordGeneral(
                        warehouseRecordMCU.id,
                        warehouseRecordMCU.name,
                        warehouseRecordMCU.count,
                        warehouseRecordMCU.location,
                        jsonDocument
                    );
            }
        }
    }
}
