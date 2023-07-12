class PointsCoefficient
{
    public static int COEFFICIENT = 3;

    public static void UpdateCoefficient()
    {
        if (!CurrentUserOptions.countTime)
        {
            if (COEFFICIENT > 1)
            {
                COEFFICIENT--;
            }
        }
        else
        {
            if(COEFFICIENT < 3)
            {
                COEFFICIENT++;
            }
        }

        if (CurrentUserOptions.infiniteChances)
        {
            if(COEFFICIENT > 1)
            {
                COEFFICIENT--;
            }
            
        }
        else
        {
            if (COEFFICIENT < 3)
            {
                COEFFICIENT++;
            }
        }
    }
}
