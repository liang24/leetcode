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