using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarehouseAccounting
{
    public static partial class Extensions
    {
        public static string ToJsonString(this JsonDocument data)
        {
            using (var stream = new MemoryStream())
            {
                Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                data.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }

    public class WarehouseRecordGeneral : WarehouseRecord
    {
        private JsonDocument data;

        public WarehouseRecordGeneral(short id, string name, short count, Location location, JsonDocument data)
            : base(id, name, count, location)
        {
            this.data = data;
        }

        public String GetDataJson()
        {
            return data.ToJsonString();
        }

        //public static implicit operator WarehouseRecordGeneral(WarehouseRecord warehouseRecord)
        //{

        //}

        public static explicit operator WarehouseRecordMCU(WarehouseRecordGeneral warehouseRecordGeneral)
        {
            if (warehouseRecordGeneral.type != TypeRecord.MCU) throw new InvalidCastException(nameof(warehouseRecordGeneral));

            JsonElement root = warehouseRecordGeneral.data.RootElement;
            WarehouseRecordMCU warehouseRecordMCU = new WarehouseRecordMCU(
                warehouseRecordGeneral.id, warehouseRecordGeneral.name, warehouseRecordGeneral.count, warehouseRecordGeneral.location,
                root.GetProperty("CPU_FREQUENCY").GetSingle(),
                root.GetProperty("SRAM").GetSingle(),
                root.GetProperty("FLASH").GetSingle(),
                root.GetProperty("EEPROM").GetSingle(),
                root.GetProperty("GPIO").GetInt32(),
                root.GetProperty("PWM_CHANNELS").GetInt32(),
                new WorkingConditions(
                        new WorkingConditions.RangeOfValues(
                                root.GetProperty("TEMPERATURE").GetProperty("MIN").GetSingle(),
                                root.GetProperty("TEMPERATURE").GetProperty("MAX").GetSingle()
                            ),
                        new WorkingConditions.RangeOfValues(
                                root.GetProperty("VOLTAGE").GetProperty("MIN").GetSingle(),
                                root.GetProperty("VOLTAGE").GetProperty("MAX").GetSingle()
                            )
                    )
                );

            return warehouseRecordMCU;
        }

        public static WarehouseRecordGeneral operator ++(WarehouseRecordGeneral obj)
        {
            obj.count++;
            return obj;

        }

        public static WarehouseRecordGeneral operator --(WarehouseRecordGeneral obj)
        {
            obj.count--;
            return obj;

        }
    }
}
