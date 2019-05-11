using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    /*Loop trough the entities,
Read pointer to entity + Bonematrix offset,
And get the float[3] position of selected bone like this:
BonePointer + 0x30 * BoneIndex + 0x0C
BonePointer + 0x30 * BoneIndex + 0x1C
BonePointer + 0x30 * BoneIndex + 0x2C*/
    public class Position
    {
        public float xAxis;
        public float yAxis;
        public float zAxis;
    }
}
