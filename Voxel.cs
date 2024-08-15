

namespace OkkamiMaker
{
    public class Voxel
    {
        // Voxel Size
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public float SizeZ { get; set; }
        // Voxel Position
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        // Constructor
        public Voxel(float sizeX = 1, float sizeY = 1, float sizeZ = 1,
                     float positionX = 0, float positionY = 0, float positionZ = 0)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
        }

        // Method to get the vertex positions of the voxel
        public List<float> Vertex()
        {
            List<float> vertexList = new List<float>
            { //      Position X              Position Y             Position Z 
                PositionX - SizeX / 2, PositionY - SizeY / 2, PositionZ - SizeZ / 2, //vertex 1
                PositionX + SizeX / 2, PositionY - SizeY / 2, PositionZ - SizeZ / 2, //vertex 2
                PositionX - SizeX / 2, PositionY + SizeY / 2, PositionZ - SizeZ / 2, //vertex 3
                PositionX + SizeX / 2, PositionY + SizeY / 2, PositionZ - SizeZ / 2, //vertex 4
                PositionX - SizeX / 2, PositionY - SizeY / 2, PositionZ + SizeZ / 2, //vertex 5
                PositionX + SizeX / 2, PositionY - SizeY / 2, PositionZ + SizeZ / 2, //vertex 6
                PositionX - SizeX / 2, PositionY + SizeY / 2, PositionZ + SizeZ / 2, //vertex 7
                PositionX + SizeX / 2, PositionY + SizeY / 2, PositionZ + SizeZ / 2  //vertex 8
            };

            return vertexList;
        }
    }
}
