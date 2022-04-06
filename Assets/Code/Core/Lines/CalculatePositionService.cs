namespace TheTalesOfTwo.Core.Lines
{
    public static class CalculatePositionService
    {
        public static float CalculateYForAvatar(float cameraSize, int line)
        {
            return CalculateY(cameraSize, line, .2f);
        }

        public static float CalculateY(float cameraSize, int line, float offsetPercent)
        {
            var height = cameraSize * 2;
            var realHeight = height * 0.9f;
            var lines = 3f;
            var pieceHeight = realHeight / lines;
            var bottomPos = -cameraSize;
            return bottomPos + pieceHeight * line - pieceHeight * (1 - offsetPercent);
        }
    }
}