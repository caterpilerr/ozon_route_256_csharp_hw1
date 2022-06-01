using System;
using System.Linq;

using MainProject.Contracts.Entities.ValueObjects;
using MainProject.Enums;

namespace MainProject.Services
{
    internal static class ItemSizeManager
    {
        public static SizeType GetSizeType(VolumeWeightData data)
        {
            var allSize = new [] 
                { data.Height, data.Length, data.Width, data.PackagedHeight, data.PackagedLength, data.PackagedWidth };

            long maxSize = allSize.Max();

            if (maxSize > 100)
            {
                return SizeType.Max;
            }

            if (Math.Max(data.PackagedWeight, data.Weight) > 10000)
            {
                return SizeType.Max;
            }

            long packagedVolume = data.PackagedHeight * data.PackagedLength * data.PackagedWidth;
            long volume = data.Height * data.Length * data.Width;
            
            long minVolume = Math.Min(packagedVolume, volume);

            if (minVolume < 10000)
            {
                return SizeType.Small;
            }

            if (minVolume < 30000)
            {
                return SizeType.Medium;
            }

            return SizeType.Max;
        }
    }
}