using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class RandomUtils {

    // 重複のない乱数を作成する。
    public static List<int> GetNoOverlapList(int min, int max, int count)
    {
        List<int> initial = new List<int>();

        for (int i = min; i < max; i++)
        {
            initial.Add(i);
        }

        List<int> result = new List<int>();
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, count - i);
            int value = initial[index];
            result.Add(value);
            initial.RemoveAt(index);
        }

        return result;
    }
}
