import "sort"

var ans [][]int
var list []int

func combinationSum2(candidates []int, target int) [][]int {
	// 1. 排序
	// 2. 回溯 + 剪枝
	ans = make([][]int, 0)
	list = make([]int, 0)
	if len(candidates) == 0 {
		return ans
	}
	sort.Ints(candidates)

	backtarck(candidates, 0, target)
	return ans
}

func backtarck(candidates []int, start int, target int) {
	// terminal
	if target == 0 {
		tmp := make([]int, len(list))
		copy(tmp, list)
		ans = append(ans, tmp)
		return
	}

	// logical code
	for i := start; i < len(candidates); i++ {
		if target-candidates[i] < 0 { // 剪枝：已排序的数组后面的数字差肯定都<0
			break
		}
		if i > start && candidates[i] == candidates[i-1] { // 去重：去掉第一个
			continue
		}
		list = append(list, candidates[i])
		backtarck(candidates, i+1, target-candidates[i])
		list = list[:len(list)-1]
	}
}