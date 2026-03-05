public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // Step 1: Create a new double array of size 'length' to hold the results.
        // Step 2: Loop from 1 to 'length' (inclusive) using index i.
        // Step 3: At each iteration, multiply 'number' by i to get the next multiple
        //         and store it in the array at position i-1.
        //         e.g. i=1 -> number*1, i=2 -> number*2, etc.
        // Step 4: Return the completed array.

        double[] result = new double[length];

        for (int i = 1; i <= length; i++)
        {
            result[i - 1] = number * i;
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // Step 1: Calculate the split index. Rotating right by 'amount' means the last
        //         'amount' elements move to the front. The split point (where the front
        //         portion begins) is data.Count - amount.
        //         e.g. data={1..9}, amount=3 -> splitIndex = 9-3 = 6
        //              so the back portion is {7,8,9} (index 6 to end)
        //              and the front portion is {1,2,3,4,5,6} (index 0 to 5)
        // Step 2: Use GetRange to extract the back portion (from splitIndex to end).
        // Step 3: Use GetRange to extract the front portion (from 0 to splitIndex).
        // Step 4: Clear the original list with RemoveRange.
        // Step 5: Add the back portion first, then the front portion using AddRange,
        //         so the list becomes {7,8,9,1,2,3,4,5,6}.

        int splitIndex = data.Count - amount;

        List<int> backPortion = data.GetRange(splitIndex, amount);
        List<int> frontPortion = data.GetRange(0, splitIndex);

        data.RemoveRange(0, data.Count);
        data.AddRange(backPortion);
        data.AddRange(frontPortion);
    }
}