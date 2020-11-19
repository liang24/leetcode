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

        for(int i = start; i <= n; i++) {
            path.Add(i);
            Backtrack(n, k, path, i+1);
            path.RemoveAt(path.Count-1);
        }
    }
}