using System;
using System.Collections;
using System.Collections.Generic;
internal class PlayerAnswers
{
    internal static ArrayList playerAnswers = new ArrayList();
    internal static ArrayList playerAnswersFields = new ArrayList();
    internal static void ClearPlayerAnswers()
    {
        playerAnswers.Clear();
        playerAnswersFields.Clear();
    }
}
