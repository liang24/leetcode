public class Solution {

    private bool[] visited;
    private char[] buffer;
    private List<string> ans;

    public string[] Permutation(string s) {
        /* 
            排列、组合、选择类题目可以使用回溯
            求解、回头使用回溯
            1. 画递归树，找状态变量
            2. 确立结束条件
            3. 找准选择列表
            4. 判断是否剪枝
            5. 作为选择，进入下一层
            6. 撤消选择
        */
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