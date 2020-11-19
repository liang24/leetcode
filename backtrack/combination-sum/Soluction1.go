import "sort"

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
	for i := start; i < len(candidates); i++ {
		if target-candidates[i] >= 0 {
			list = append(list, candidates[i])
			backtrack(candidates, target-candidates[i], i) // 允许重复使用当前数字
			list = list[:len(list)-1]
		} else { // 剪枝：target-candidates[i] < 0 表示结果集加上 candidates[i] 将超过 target，不符合条件，剪掉。
			break
		}
	}
}