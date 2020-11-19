### [剑指 Offer 38. 字符串的排列](https://leetcode-cn.com/problems/zi-fu-chuan-de-pai-lie-lcof)

输入一个字符串，打印出该字符串中字符的所有排列。

你可以以任意顺序返回这个字符串数组，但里面不能有重复元素。

**示例:**

```
输入：s = "abc"
输出：["abc","acb","bac","bca","cab","cba"]
```

**限制：**

```
1 <= s 的长度 <= 8
```

### 解题思路

题目是排列组合类问题，使用回溯 + 剪枝的解法。

求解步骤：

1. 画递归树，找状态变量：当前字符数组 `buffer` 、已选择字符状态 `visited`
2. 确立结束条件：当前字符数组 `buffer` 长度为字符串 `s` 的长度
3. 找准选择列表：字符串 `s` 的字符数组
4. 判断是否剪枝：题目要求不能重复，因此要对递归树进行剪枝，剪枝条件有两个：
    - 已选择的字符不能再选： `visited[i]`
    - 第二个条件比较不好发现，因为字符串中的字符是可能重复的，只靠第一个条件不能完全排除重复的字符串，通过递归树可以发现，当字符与前一个字符相同，并且前一个字符已选择的情况下，要剪枝。前提是字符数组是有序的。因此得到条件为 `i > 0 && visited[i-1] && chars[i] == chars[i-1]`
5. 作为选择，进入下一层
6. 撤消选择

### 代码

```csharp
public class Solution {

    private bool[] visited;
    private char[] buffer;
    private List<string> ans;

    public string[] Permutation(string s) {
        int n = s.Length;
        visited = new bool[n];
        buffer = new char[n];
        ans = new List<string>();

        char[] chars = s.ToCharArray(); // 排序
        Array.Sort(chars);

        Backtrack(chars, 0);

        return ans.ToArray();
    }

    public void Backtrack(char[] chars, int level) {
        if(level == chars.Length) {
            ans.Add(new string(buffer));
            return;
        }

        for(int i = 0; i < chars.Length; i++) {
            if(visited[i]) {    //  剪枝：字符已选择过，跳出
                continue;
            }

            if(i > 0 && !visited[i-1] && chars[i] == chars[i-1]) { // 剪枝：
                continue;
            }
            
            // 作出选择
            visited[i] = true;
            buffer[level] = chars[i];

            // 进入下一层
            Backtrack(chars, level+1);
            
            // 撤消选择
            buffer[level] = ' ';
            visited[i] = false;
        }
    }
}
```

### 复杂度分析

- 时间复杂度：$O(N!)$ 。$N$ 为字符串 s 的长度；时间复杂度和字符串排列的方案数成线性关系，方案数为 $N×(N−1)×(N−2)…×2×1$ ，因此复杂度为 $O(N!)$ 。
- 空间复杂度：$O(N!)$ 。 全排列的递归深度为 $N$ ，系统累计使用栈空间大小为 $O(N)$ ；结果集 `ans` 最多存储的字符串数量为递归树的叶子节点数，$N×(N−1)×(N−2)…×2×1$，即 $O(N!)$ 的额外空间。