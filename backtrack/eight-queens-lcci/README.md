### [面试题 08.12. 八皇后](https://leetcode-cn.com/problems/eight-queens-lcci/)

设计一种算法，打印 N 皇后在 N × N 棋盘上的各种摆法，其中每个皇后都不同行、不同列，也不在对角线上。这里的“对角线”指的是所有的对角线，不只是平分整个棋盘的那两条对角线。

注意：本题相对原题做了扩展

**示例:**

```
输入：4
 输出：[[".Q..","...Q","Q...","..Q."],["..Q.","Q...","...Q",".Q.."]]
 解释: 4 皇后问题存在如下两个不同的解法。
[
 [".Q..",  // 解法 1
  "...Q",
  "Q...",
  "..Q."],

 ["..Q.",  // 解法 2
  "Q...",
  "...Q",
  ".Q.."]
]
```

### 解题思路

题目是排列问题，使用回溯算法。

#### 1. 画递归树，找变量

![eight-queens-lcci.png](https://pic.leetcode-cn.com/1605756217-fNrNjN-eight-queens-lcci.png)

观察递归树，皇后 `Q` 在每一行的每一列都尝试放置，需要一变量 `row` 保存当前处理的是哪行。

```csharp
// path 表示临时保存的结果
// i 表示当前选择 digits 的字符下标
void Backtrack(int n, char[][] path, int row);
```

#### 2. 找结束条件

观察递归树，当 `n` 行都遍历完，结束遍历，因此结束条件为 `row == n `。

```csharp
if(row == n) {
    IList<string> tmp = new List<string>();
    for(int i = 0; i < n; i++) {
        tmp.Add(new string(path[i]));
    }
    ans.Add(tmp);
    return;
}
```

#### 3. 找选择列表

```csharp
// 遍历每一列，尝试放置
for(int col = 0; col < n; col++) {
    
}
```

#### 4. 断是否需要剪枝

从递归树可知，有不符合条件的结果，因此需要剪枝。根据题目条件可知，每个皇后放置的位置要满足三个条件：

1. 每个皇后不同行；
2. 每个皇后不同列；
3. 每个皇后不在对角线；

因此定义一个函数来判断是否允许放置，如果不允许，则剪枝。

```csharp
public bool IsValid(char[][] path, int n, int row, int col) {
    // 判断是否在同一行
    for(int i = 0; i < n; i++) {
        if(i != col && path[row][i] == 'Q') {
            return false;
        }
    }

    // 判断是否在同一列
    for(int i = 0; i < n; i++) {
        if(i != row && path[i][col] == 'Q') {
            return false;
        }
    }

    // 判断是否在左对角线
    for(int i = 0; i < n; i++) {
        int j = i - row + col;
        if(i != row && j >= 0 && j < n && path[i][j] == 'Q') {
            return false;
        }
    }

    // 判断是否在右对角线
    for(int i = 0; i < n; i++) {
        int j = row - i + col;
        if(i != row && j >= 0 && j < n && path[i][j] == 'Q') {
            return false;
        }
    }
    return true;
}
```

#### 5. 做出选择(即for 循环里面的)

```csharp
for(int col = 0; col < n; col++) {
    path[row][col] = 'Q';             // 做出选择
    if(IsValid(path, n, row, col)) {  // 判断位置是否可放
        Backtrack(n, path, row+1);  // 执行下一层
    }
}
```

#### 6. 撤消选择

```csharp
for(int col = 0; col < n; col++) {
    path[row][col] = 'Q';             // 做出选择
    if(IsValid(path, n, row, col)) {  // 判断位置是否可放
        Backtrack(n, path, row+1);  // 执行下一层
    }
    path[row][col] = '.';             // 撤回选择
}
```

### 代码

```csharp
public class Solution {
    private IList<IList<string>> ans;
    public IList<IList<string>> SolveNQueens(int n) {
        ans = new List<IList<string>>();
        if(n <= 0) {
            return ans;
        }

        // 初始化棋盘
        char[][] path = new char[n][];
        for(int i = 0; i < n; i++) {
            path[i] = new char[n];
            for(int j = 0; j < n; j++) {
                path[i][j] = '.';
            }
        }

        Backtrack(n, path, 0);
        return ans;
    }

    public void Backtrack(int n, char[][] path, int row) {
        if(row == n) {
            IList<string> tmp = new List<string>();
            for(int i = 0; i < n; i++) {
                tmp.Add(new string(path[i]));
            }
            ans.Add(tmp);
            return;
        }

        for(int col = 0; col < n; col++) {
            path[row][col] = 'Q';               // 做出选择
            if(IsValid(path, n, row, col)) {    // 判断位置是否可放
                Backtrack(n, path, row+1);      // 执行下一层
            }
            path[row][col] = '.';               // 撤回选择
        }
    }

    public bool IsValid(char[][] path, int n, int row, int col) {
        // 判断是否在同一列
        for(int i = 0; i < n; i++) {
            if(i != row && path[i][col] == 'Q') {
                return false;
            }
        }

        // 判断是否在左对角线
        for(int i = 0; i < n; i++) {
            int j = i - row + col;
            if(i != row && j >= 0 && j < n && path[i][j] == 'Q') {
                return false;
            }
        }

        // 判断是否在右对角线
        for(int i = 0; i < n; i++) {
            int j = row - i + col;
            if(i != row && j >= 0 && j < n && path[i][j] == 'Q') {
                return false;
            }
        }
        return true;
    }
}
```

### 复杂度分析

- 时间复杂度：$O(n*n!)$。递归树的节点数为 $1+n+n^2+...+n^(n-1)+n^n = n!$，一共要调用 $n!$ 次 `Backtrack` 函数，而函数 `Backtrack` 的时间复杂度为 `O(n)`
- 空间复杂度：$O(n)$。递归树的层级为 $n$，而递归需要额外的空间，因此空间复杂度为 $O(n)$。

### 优化

函数 `IsValid` 在判断时，一共进行了3次遍历，很耗时。可以通过空间换时间来降低时间复杂度，通过数组来记录列、对角线和反对角线的占位情况。

```csharp
private bool[] cols, dgs, udgs; // dgs 表示对角线；udgs 表示反对角线

public bool IsValid(int n, int row, int col) {
    return !cols[col] && !dgs[row + col] && !udgs[n - row + col];
}

for(int col = 0; col < n; col++) {
    if(IsValid(path, n, row, col)) {  // 判断位置是否可放
        path[row][col] = 'Q';             // 做出选择
        cols[col] = dgs[row + col] = udgs[n - row + col] = true;
        Backtrack(n, path, row+1);  // 执行下一层
        cols[col] = dgs[row + col] = udgs[n - row + col] = false;
        path[row][col] = '.';             // 撤回选择
    }    
} 
```

### 代码



```csharp
public class Solution {
    private IList<IList<string>> ans;
    private bool[] cols, dgs, udgs; // dgs 表示对角线；udgs 表示反对角线
    
    public IList<IList<string>> SolveNQueens(int n) {
        ans = new List<IList<string>>();
        if(n <= 0) {
            return ans;
        }

        // 初始化棋盘
        char[][] path = new char[n][];
        for(int i = 0; i < n; i++) {
            path[i] = new char[n];
            for(int j = 0; j < n; j++) {
                path[i][j] = '.';
            }
        }

        cols = new bool[n];
        dgs = new bool[2*n];
        udgs = new bool[2*n];

        Backtrack(n, path, 0);
        return ans;
    }

    public void Backtrack(int n, char[][] path, int row) {
        if(row == n) {
            IList<string> tmp = new List<string>();
            for(int i = 0; i < n; i++) {
                tmp.Add(new string(path[i]));
            }
            ans.Add(tmp);
            return;
        }

        for(int col = 0; col < n; col++) {
            if(IsValid(n, row, col)) {  // 判断位置是否可放
                path[row][col] = 'Q';             // 做出选择
                cols[col] = dgs[row + col] = udgs[n - row + col] = true;
                Backtrack(n, path, row+1);  // 执行下一层
                cols[col] = dgs[row + col] = udgs[n - row + col] = false;
                path[row][col] = '.';             // 撤回选择
            }    
        } 
    }

    public bool IsValid(int n, int row, int col) {
        return !cols[col] && !dgs[row + col] && !udgs[n - row + col];
    }
}
```

### 复杂度分析

- 时间复杂度：$O(n!)$。递归树的节点数为 $1+n+n^2+...+n^(n-1)+n^n = n!$，一共要调用 $n!$ 次 `Backtrack` 函数，而函数 `Backtrack` 的时间复杂度为 $O(1)$。
- 空间复杂度：$O(n)$。递归树的层级为 $n$，而递归需要额外的空间，因此空间复杂度为 $O(n)$。辅助判断的数组的空间复杂度为 $O(n)$。