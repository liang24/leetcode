### [17. 电话号码的字母组合](https://leetcode-cn.com/problems/letter-combinations-of-a-phone-number/)

给定一个仅包含数字 2-9 的字符串，返回所有它能表示的字母组合。

给出数字到字母的映射如下（与电话按键相同）。注意 1 不对应任何字母。

**示例:**

```
输入："23"
输出：["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
```

**说明:**

尽管上面的答案是按字典序排列的，但是你可以任意选择答案输出的顺序。

### 解题思路

题目是组合问题，使用回溯算法。

#### 1. 画递归树，找变量

![letter-combinations-of-a-phone-number.png](https://pic.leetcode-cn.com/1605748125-ZxIutl-letter-combinations-of-a-phone-number.png)

观察递归树，字符串 `digits` 中的每个字符都要选择一次，遍历所有字符，因此需要一参数( `i` )来记录当前选择的字符下标。

```csharp
// path 表示临时保存的结果
// i 表示当前选择 digits 的字符下标
void Backtrack(string digits, List<char> path, int i);
```

#### 2. 找结束条件

第1步可知，字符串 `digits` 遍历结束，即 `i` 超出下标边界。

```csharp
if(i == digits.Length) {
    ans.Add(new string(path.ToArray()));
    return;
}
```

#### 3. 找选择列表

选择列表根据当前选择的字符所对应的电话按键的字符集，为了方便使用，使用字典来保存数字字符与字母字符集的映射关系。

```csharp
private Dictionary<char, string> dict;
public Solution(){
    dict = new Dictionary<char, string>();
    dict['2'] = "abc";
    dict['3'] = "def";
    dict['4'] = "ghi";
    dict['5'] = "jkl";
    dict['6'] = "mno";
    dict['7'] = "pqrs";
    dict['8'] = "tuv";
    dict['9'] = "wxyz";
}

public void Backtrack(string digits, List<char> path, int i) {
    // ...
    foreach(char c in dict[digits[i]]) {    // 选择列表
        // do something...
    }
    // ...
}
```

#### 4. 断是否需要剪枝

从递归树可知，没有重复的路径，因此不需要剪枝。

#### 5. 做出选择(即for 循环里面的)

```csharp
public void Backtrack(string digits, List<char> path, int i) {
    if(i == digits.Length) {
        ans.Add(new string(path.ToArray()));
        return;
    }

    foreach(char c in dict[digits[i]]) {
        path.Add(c);    // 做出选择
        Backtrack(digits, path, i+1);   // 执行下一层
    }
}
```

#### 6. 撤消选择

```csharp
public void Backtrack(string digits, List<char> path, int i) {
    if(i == digits.Length) {
        ans.Add(new string(path.ToArray()));
        return;
    }

    foreach(char c in dict[digits[i]]) {
        path.Add(c);
        Backtrack(digits, path, i+1);
        path.RemoveAt(path.Count-1);    // 撤消选择
    }
}
```

### 代码

```csharp
public class Solution {
    private IList<string> ans;
    private Dictionary<char, string> dict;

    public Solution(){
        dict = new Dictionary<char, string>();
        dict['2'] = "abc";
        dict['3'] = "def";
        dict['4'] = "ghi";
        dict['5'] = "jkl";
        dict['6'] = "mno";
        dict['7'] = "pqrs";
        dict['8'] = "tuv";
        dict['9'] = "wxyz";
    }

    public IList<string> LetterCombinations(string digits) {
        // dfs
        ans = new List<string>();
        if(digits == null || digits.Length == 0) {
            return ans;
        }

        Backtrack(digits, new List<char>(), 0);

        return ans;
    }

    public void Backtrack(string digits, List<char> path, int i) {
        if(i == digits.Length) {
            ans.Add(new string(path.ToArray()));
            return;
        }

        foreach(char c in dict[digits[i]]) {
            path.Add(c);

            Backtrack(digits, path, i+1);

            path.RemoveAt(path.Count-1);
        }
    }
}
```

### 复杂度分析

- 时间复杂度：$O(3^m*2^n)$。$m$ 是对应三个字母的数字个数，$n$ 是对应四个字母的数字个数
- 空间复杂度：$O(m+n)$。$m+n$ 表示字符串长度，而递归树的层级与字符串长度一样，而递归需要额外的空间，因此空间复杂度为 $O(m+n)$。