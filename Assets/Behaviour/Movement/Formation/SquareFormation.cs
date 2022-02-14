using UnityEngine;

namespace JHiga.RTSEngine.AI.Formation
{
    public class SquareFormation : BaseFormation
    {
        private float distance;
        private int maxColumns;
        public SquareFormation(float distance, int maxColumns = 0)
        {
            this.distance = distance;
            this.maxColumns = maxColumns;
        }
        public override Vector3 GetPosition(Vector3 position, IExtendableEntity entity, int index, int count)
        {
            if (count == 1)
                return position;

            int columns;
            if (maxColumns == 0)
                columns = Mathf.CeilToInt(Mathf.Floor(Mathf.Sqrt(count)));
            else
                columns = Mathf.Clamp(count, 0, maxColumns);

            float xOffset = (columns * distance) / 4;
            float zOffset = (count / columns * distance) / 4;

            return new Vector3((index % columns) * distance + position.x - xOffset, position.y, (index / columns) * distance + position.z - zOffset);
        }
    }
}
