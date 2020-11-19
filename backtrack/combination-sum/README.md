## 前言

看完这篇题解，可以再看看这几个问题：
- [39. 组合总和](https://leetcode-cn.com/problems/combination-sum/solution/hui-su-jian-zhi-go-by-da-za-cao-2/)
- [40. 组合总和 II](https://leetcode-cn.com/problems/combination-sum-ii/solution/hui-su-jian-zhi-by-da-za-cao/)
- [216. 组合总和 III](https://leetcode-cn.com/problems/combination-sum-iii/solution/zu-he-zong-he-iii-hui-su-jian-zhi-go-by-da-za-cao/)
- [377. 组合总和 Ⅳ.](https://leetcode-cn.com/problems/combination-sum-iv/solution/hui-su-ji-yi-hua-dong-tai-gui-hua-go-by-da-za-cao/)

## 题目描述

![39. 组合总和](https://pic.leetcode-cn.com/1599813845-kkmUcv-combination-sum.png)

题目：[39. 组合总和](https://leetcode-cn.com/problems/combination-sum/)

## 解题思路

算法步骤：
1. 排序。方便进行剪枝判断。
2. 回溯。排列、组合类题目常用方法。
3. 剪枝。提前去掉不符合的结果，降低时间复杂度。

## 代码实现

```golang
var ans [][]int
var list []int

func combinationSum(candidates []int, target int) [][]int {
    /*  回溯+剪枝
        1. 排序
        2. 回溯 + 剪枝
    */ 
    ans = make([][]int, 0)
    list = make([]int, 0)
    sort.Ints(candidates)
    backtrack(candidates, target, 0)
    return ans
}

func backtrack(candidates []int, target int, start int) {
    if target == 0 { // 得到结果
        tmp := make([]int, len(list))
        copy(tmp, list)
        ans = append(ans, tmp)
        return
    }
    for i:=start;i<len(candidates);i++ {
        if target-candidates[i] >= 0 { 
            list = append(list, candidates[i])
            backtrack(candidates, target-candidates[i], i) // 允许重复使用当前数字
            list = list[:len(list)-1]
        } else { // 剪枝：target-candidates[i] < 0 表示结果集加上 candidates[i] 将超过 target，不符合条件，剪掉。
            break
        }
    }
}
```

复杂度分析：
- 时间复杂度：$O(2^n * n)$，其中 `n` 是数组 `candidates` 的长度。一般的递归树的时间复杂度是 $O(2^n)$，遍历数组的时间复杂度是 $O(n)$ ，最终时间复杂度为 $O(2^n * n)$。
- 空间复杂度：$O(n)$。除了保存结果的数组外，还需要一个数组保存回溯过程的中间结果。
