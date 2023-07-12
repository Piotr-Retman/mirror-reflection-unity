using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PointCounter
 {

    public int CountPoints(int actualPoints, int answersCounted)
    {
        return actualPoints+(10*PointsCoefficient.COEFFICIENT*answersCounted);
    }

    internal int CountBonus(int actualPoints)
    {
        SumUpLevelData.currentLevelPoints = actualPoints.ToString();

        var bonus = 0;
        if (CurrentUserOptions.countTime)
        {
            bonus = 10 * CurrentLevelData.levelChances + (int)CurrentLevelData.secondsLeft * 10;
            actualPoints = actualPoints + bonus;
        }
        else
        {
            bonus = 10 * CurrentLevelData.levelChances;
            actualPoints = actualPoints + bonus;
        }

        SumUpLevelData.bonusValue = bonus.ToString();
        SumUpLevelData.sumBonusAndLevelPoints = actualPoints.ToString();

        return actualPoints;
    }
}

