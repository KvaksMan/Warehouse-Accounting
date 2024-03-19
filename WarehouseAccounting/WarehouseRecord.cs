using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WarehouseAccounting.WarehouseRecord;

namespace WarehouseAccounting
{
    public abstract class WarehouseRecord
    {
        public enum TypeRecord
        {
            GENERAL = 0,
            OTHER,
            MCU,
            SENSOR,
            MODULE,
        }

        public class Location
        {
            int box;
            int column;
            int row;

            public Location(int box, int column, int row)
            {
                this.box = box;
                this.column = column;
                this.row = row;
            }

            public Location(int code)
            {
                this.row = code % 100;
                code /= 100;
                this.column = code % 100;
                code /= 100;
                this.box = code;
            }

            public int GetCode()
            {
                return (this.box * 100 + this.column) * 100 + this.row;
            }

            public static implicit operator Location(int code)
            {
                return new Location(code);
            }

            public static implicit operator int(Location location)
            {
                return location.GetCode();
            }
        }

        public short      id       { get; }
        public string     name     { get; }
        public TypeRecord type     { get; }
        public short      count    { get; set; }
        public Location   location { get; set; }

        protected WarehouseRecord(short id, string name, short count, Location location)
        {
            this.id    = id;
            this.name  = name;
            this.count = count;
            this.type = (TypeRecord)Enum.ToObject(typeof(TypeRecord), (id / 1000));
            this.location = location;
        }
    }
}
