### [77. 组合](https://leetcode-cn.com/problems/combinations/)

给定两个整数 n 和 k，返回 1 ... n 中所有可能的 k 个数的组合。

**示例:**

```
输入: n = 4, k = 2
输出:
[
  [2,4],
  [3,4],
  [2,3],
  [1,2],
  [1,3],
  [1,4],
]
```

### 解题思路

题目是组合问题，使用回溯算法。

#### 1. 画递归树，找变量

![combinations.png](https://pic.leetcode-cn.com/1605749970-URybFi-combinations.png)

观察递归树，当前的选择列表与上一层的选择有关，即为上一层选择后的数字为当前的选择列表，例如上一层选择了 `2` ，那么当前的选择列表为 `[3, 4]` 。因此增加一参数 `start` 表示当前的选择列表是从多少开始。

```csharp
// path 表示临时保存的结果
void Backtrack(int nums, int k, List<int> path, int start);
```

#### 2. 找结束条件

第1步可知，路径的长度与数字 `k` 相等。

```csharp
if(path.Count == k) {
    ans.Add(new List<int>(path));
    return;
}
```

#### 3. 找选择列表

从递归树可知，当前的选择列表除了与上一层的选择有关，也与选择数 `k` 有关，例如，选择 `4` 会发现后面没有选择了，因此选择列表的可选范围是 `[start, n-k+1]`。

```csharp
for(int i = start; i <= n - k + 1; i++) {
    // do something...
}
```

#### 4. 断是否需要剪枝

从递归树可知，没有重复的路径，因此不需要剪枝。

#### 5. 做出选择(即for 循环里面的)

```csharp
public void Backtrack(int n, int k, List<int> path, int start) {
    if(path.Count == k) { // 已选够了
        ans.Add(new List<int>(path));
        return;
    }

    for(int i = start; i <= n - k + 1; i++) {
        path.Add(i);                // 做出选择
        Backtrack(n, k, path, i+1); // 执行下一层，并且把下一层的选择列表的开始值，即 i+1 ，传到下一层
    }
}
```

#### 6. 撤消选择

```csharp
public void Backtrack(int n, int k, List<int> path, int start) {
    if(path.Count == k) { // 已选够了
        ans.Add(new List<int>(path));
        return;
    }

    for(int i = start; i <= n - k + 1; i++) {
        path.Add(i);                // 做出选择
        Backtrack(n, k, path, i+1); // 执行下一层，并且把下一层的选择列表的开始值，即 i+1 ，传到下一层
        path.RemoveAt(path.Count-1);// 撤消选择
    }
}
```

### 代码

```csharp
public class Solution {
    private IList<IList<int>> ans;

    public IList<IList<int>> Combine(int n, int k) {
        ans = new List<IList<int>>();
        if(n <= 0 || k == 0) {
            return ans;
        }
        Backtrack(n, k, new List<int>(), 1);
        return ans;
    }

    public void Backtrack(int n, int k, List<int> path, int start) {
        if(path.Count == k) { // 已选够了
            ans.Add(new List<int>(path));
            return;
        }

        for(int i = start; i <= n - k + 1; i++) {
            path.Add(i);                // 做出选择
            Backtrack(n, k, path, i+1); // 执行下一层，并且把下一层的选择列表的开始值，即 i+1 ，传到下一层
            path.RemoveAt(path.Count-1);// 撤消选择
        }
    }
}
```

### 复杂度分析

- 时间复杂度：$O(k*n!)$。递归树的结点总数为 $1+n+n^2+....+n^(n-1)+n^n=n!$ ，即调用了 $n!$ 次 `Backtrack` 函数，而 `Backtrack` 函数的时间复杂度为 $O(k)$ ，因为需要 $O(k)$ 的时间来复制 `path`。最终的时间复杂度为 $O(k*n!)$。
- 空间复杂度：$O(k)$。除答案数组以外，递归函数在递归过程中需要为每一层递归函数分配栈空间，所以这里需要额外的空间且该空间取决于递归的深度，这里可知递归调用深度为 $O(k)$。