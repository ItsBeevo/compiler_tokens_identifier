using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace JASON_PARSER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Table = new DataTable("Lexemes Table");
            DataColumn dc1 = new DataColumn("lexeme", typeof(String));
            DataColumn dc2 = new DataColumn("token", typeof(String));

            DataTable ErrorsTable = new DataTable("Errors Table");
            DataColumn ec1 = new DataColumn("error lexeme", typeof(String));
            ErrorsTable.Columns.Add(ec1);


            Parser parser = new Parser();
            TokensIdentifier tokensIdentifier = new TokensIdentifier();


            List<string> lexemes = parser.parseSourceCode(richTextBox1.Text.ToString());
            System.Diagnostics.Debug.WriteLine(lexemes.Count.ToString());
            List<TokenEnums> tokens = tokensIdentifier.identifyTokens(lexemes);

            Table.Columns.Add(dc1);
            Table.Columns.Add(dc2);

            for (int i = 0; i < lexemes.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(lexemes[i].ToString());
                DataRow dr = Table.NewRow();
                DataRow edr = ErrorsTable.NewRow();

                dr[0] = lexemes[i].ToString();
                edr[0] = lexemes[i].ToString();
             
                if (TokensDictionary.tokenEnumToOutput.ContainsKey(tokens[i]))
                {
                    dr[1] = TokensDictionary.tokenEnumToOutput[tokens[i]];

                    if (dr[1] == TokensDictionary.tokenEnumToOutput[TokenEnums.error])
                    {
                        ErrorsTable.Rows.Add(edr);
                    }
                }
                else
                {
                    dr[1] = TokensDictionary.tokenEnumToOutput[TokenEnums.error];
                    edr[0] = TokensDictionary.tokenEnumToOutput[TokenEnums.error];
                    ErrorsTable.Rows.Add(edr);
                }

              
                Table.Rows.Add(dr);
            }

            dataGridView1.DataSource = Table;
            dataGridView2.DataSource = ErrorsTable;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class Token
    {
        public string lexeme;
        public TokenEnums tokenEnum;
    }

    public static class Splitters
    {
        public static List<char> ignoreChars = new List<char>() { ' ', '\r', '\t', '\n' };
        public static List<string> ignoreStrings = new List<string>() { "", " ", "\t", "\n", "\r" };

        public static List<string> operators = new List<string>() { "+", "-", "/", "*", "=" };
        public static List<string> brackets = new List<string>() { "{", "}", "(", ")", "[", "]" };
        public static List<string> delemeters = new List<string>() { ".", ";", "," };
        public static List<string> comperators = new List<string>() { ">", "<", "=" };

        public static List<string> splitters = new List<string>() 
        {
            "+", "-", "/", "*",
            "{", "}", "(", ")",
            "[", "]", ".", ";",
            ",", "=" ,">", "<", "="
        };
    }

    public enum TokenEnums
    {
        beginToken, endifToken, parametersToken, setToken,
        callToken, endUntilToken, procedureToken, thenToken,
        declareToken, endWhileToken, programToken, untilToken,
        doToken, ifToken, readToken, whileToken,
        elseToken, integerToken, realToken, writeToken,
        endToken,

        constantNumber, identifier,
        oppenedCurlyBracked, closedCurlyBrakcet,
        oppenedCircleBracket, closedCircleBracket,
        oppenedSquareBracket, closedSquareBracket,

        plus, minus, multipy, divide,

        greaterThan, lessThan, isequal,
        comma, semicolon, dot,

        returnToken, main,

        error , unrecognizedToken,

        intToken , floatToken , stringToken ,
        repeat ,elseif , endl , assign,
        or,orr,and,andd
    }

    public static class TokensDictionary
    {
        public static Dictionary<string, TokenEnums> inputToTokenEnum = new Dictionary<string, TokenEnums>
        {
            {"int" , TokenEnums.intToken},
            {"float" , TokenEnums.floatToken},
            {"string" , TokenEnums.stringToken},

            {"repeat" , TokenEnums.repeat},
            {"elseif" , TokenEnums.elseif},
            {"endl" , TokenEnums.endl},
            {":=" , TokenEnums.assign},

            {"|" , TokenEnums.or},
            {"||" , TokenEnums.orr},
            {"&" , TokenEnums.and},
            {"&&" , TokenEnums.andd},

            {"return" , TokenEnums.returnToken},
            {"main" , TokenEnums.main},

            {"begin" , TokenEnums.beginToken},
            {"endif" , TokenEnums.endifToken},
            {"parameters" , TokenEnums.parametersToken},
            {"set" , TokenEnums.setToken},

            {"call" , TokenEnums.callToken},
            {"enduntil" , TokenEnums.endUntilToken},
            {"procedure" , TokenEnums.procedureToken},
            {"then" , TokenEnums.thenToken},

            {"declare" , TokenEnums.declareToken},
            {"endwhile" , TokenEnums.endWhileToken},
            {"program" , TokenEnums.programToken},
            {"until" , TokenEnums.untilToken},

            {"do" , TokenEnums.doToken},
            {"if" , TokenEnums.ifToken},
            {"read" , TokenEnums.readToken},
            {"while" , TokenEnums.whileToken},

            {"else" , TokenEnums.elseToken},
            {"integer" , TokenEnums.integerToken},
            {"real" , TokenEnums.realToken},
            {"write" , TokenEnums.writeToken},

            {"end" , TokenEnums.endToken},

            {"+" , TokenEnums.plus},
            {"-" , TokenEnums.minus},
            {"/" , TokenEnums.divide},
            {"*" , TokenEnums.multipy},

            {">" , TokenEnums.greaterThan},
            {"=" , TokenEnums.isequal},
            {"<" , TokenEnums.lessThan},

            {"." , TokenEnums.dot},
            {";" , TokenEnums.semicolon},
            {"," , TokenEnums.comma},

            {"{" , TokenEnums.oppenedCurlyBracked},
            {"}" , TokenEnums.closedCurlyBrakcet},

            {"(" , TokenEnums.oppenedCircleBracket},
            {")" , TokenEnums.closedCircleBracket},

            {"[" , TokenEnums.oppenedSquareBracket},
            {"]" , TokenEnums.closedSquareBracket},

            {">=" , TokenEnums.unrecognizedToken},
            {"<=" , TokenEnums.unrecognizedToken},

        };

        public static Dictionary<TokenEnums, string> tokenEnumToOutput = new Dictionary<TokenEnums, string>
        {
            {TokenEnums.intToken, "T_int" },
            {TokenEnums.floatToken, "T_float" },
            {TokenEnums.stringToken, "T_string" },

            {TokenEnums.repeat, "T_repeat" },
            {TokenEnums.elseif, "T_elseif" },
            {TokenEnums.assign, "T_assing" },

            {TokenEnums.or, "T_bitwiseOr" },
            {TokenEnums.and, "T_bitwiseAnd" },

            {TokenEnums.orr, "T_logicalOr" },
            {TokenEnums.andd, "T_logicalAnd" },


            {TokenEnums.unrecognizedToken , "T_unrecognized"},
            {TokenEnums.returnToken , "T_return" },
            {TokenEnums.main , "T_main" },

            {TokenEnums.beginToken , "T_begin"},
            {TokenEnums.endifToken , "T_endif" },
            {TokenEnums.parametersToken ,  "T_parameter"},
            {TokenEnums.setToken , "T_token" },

            {TokenEnums.callToken , "T_call"},
            {TokenEnums.endUntilToken ,"T_enduntil" },
            {TokenEnums.procedureToken, "T_procedure"},
            {TokenEnums.thenToken , "T_then"},

            {TokenEnums.declareToken , "T_declare" },
            {TokenEnums.endWhileToken , "T_endwhile" },
            {TokenEnums.programToken , "T_program" },
            {TokenEnums.untilToken , "T_untilToken"},

            {TokenEnums.doToken, "T_do"},
            {TokenEnums.ifToken , "T_if"},
            {TokenEnums.readToken , "T_read"},
            {TokenEnums.whileToken , "T_while"},

            {TokenEnums.elseToken , "T_else"},
            {TokenEnums.integerToken , "T_integer"},
            {TokenEnums.realToken , "T_real"},
            {TokenEnums.writeToken , "T_write"},

            {TokenEnums.endToken , "T_end"},

            {TokenEnums.plus , "T_plus_op"},
            {TokenEnums.minus , "T_minus_op"},
            {TokenEnums.multipy , "T_multipy_op"},
            {TokenEnums.divide , "T_divide_op"},

            {TokenEnums.greaterThan , "T_greatherThan"},
            {TokenEnums.isequal , "T_isEqualOp"},
            {TokenEnums.lessThan , "T_lessThan"},

            {TokenEnums.semicolon , "T_semiColon"},
            {TokenEnums.comma , "T_comma"},
            {TokenEnums.dot , "T_dot"},

            {TokenEnums.oppenedCurlyBracked , "T_LCurly" },
            {TokenEnums.closedCurlyBrakcet , "T_RCurly" },

            {TokenEnums.oppenedCircleBracket , "T_LCircle"},
            {TokenEnums.closedCircleBracket , "T_RCircle"},

            {TokenEnums.oppenedSquareBracket, "T_LSquare" },
            {TokenEnums.closedSquareBracket, "T_RSquare" },

            {TokenEnums.error, "T_error" },
            {TokenEnums.constantNumber , "T_constantNumber"},
            {TokenEnums.identifier , "T_identifier"},
        };
    }

    public class Parser
    {
        Regex NumberRegex = new Regex(@"^[0-9]+(\.[0-9]*)?$");
        Regex IdentifierRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
        public string filterString(string str)
        {
            string newStr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (Splitters.ignoreChars.Contains(str[i]))
                {
                    continue;
                }
                newStr += str[i].ToString();
            }
            return newStr;
        }
        public List<string> formatComplexTokens(string str)
        {
            string token = "";
            List<string> output = new List<string>();


            for (int i = 0; i < str.Length; i++)
            {
                if (Splitters.splitters.Contains(str[i].ToString()))
                {
                    output.Add(token);
                    token = "";
                    output.Add(str[i].ToString());
                    continue;
                }
                else
                {
                    token += str[i].ToString();
                }
            }
            if (token.Length > 0)
            {
                output.Add(token);
            }
            return output;
        }

        public List<string> filterTokens(List<string> tokens)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < tokens.Count; i++)
            {
                // bonus >= check
                if (tokens[i] == ">")
                {
                    if (i < tokens.Count - 1)
                    {
                        if (tokens[i + 1] == "=")
                        {
                            output.Add(">=");
                            i += 1;
                            continue;
                        }
                    }
                }

                // no double operator as ** ++ -- //
                if (Splitters.operators.Contains(tokens[i]))
                {
                    if (i < tokens.Count - 1)
                    {
                        if (Splitters.operators.Contains(tokens[i+1]))
                        {
                            string str = "";
                            str += tokens[i].ToString();
                            str += tokens[i+1].ToString();
                            output.Add(str);
                            i += 1;
                            continue;
                        }
                    }
                }

                // no two numbers 
                if (TokensIdentifier.isConstantNumber(tokens[i]))
                {
                    if (i < tokens.Count - 1)
                    {
                        if (TokensIdentifier.isConstantNumber(tokens[i + 1]))
                        {
                            string str = "";
                            str += tokens[i].ToString();
                            str += ' '.ToString();
                            str += tokens[i + 1].ToString();
                            output.Add(str);
                            i += 1;
                            continue;
                        }
                    }
                }
                // no two comparisons
                if (Splitters.comperators.Contains(tokens[i]))
                {
                    if (i < tokens.Count - 1)
                    {
                        if (Splitters.comperators.Contains(tokens[i + 1]))
                        {
                            string str = "";
                            str += tokens[i].ToString();
                            str += tokens[i + 1].ToString();
                            output.Add(str);
                            i += 1;
                            continue;
                        }
                    }
                }

                // no two vars
                if (!TokensIdentifier.isReservedWord(tokens[i]) && TokensIdentifier.isIdentifier(tokens[i]))
                {
                    if (i < tokens.Count - 1)
                    {
                        if (TokensIdentifier.isIdentifier(tokens[i + 1]))
                        {
                            string str = "";
                            str += tokens[i].ToString();
                            str += ' '.ToString();
                            str += tokens[i + 1].ToString();
                            output.Add(str);
                            i += 1;
                            continue;
                        }
                    }
                }



                output.Add(tokens[i]);
            }

            return output;
        }
        public List<string> parseSourceCode(string sourceCode)
        {
            List<string> tokens = new List<string>();
            string[] splittedCode = sourceCode.Split('\n');

            for (int j = 0; j < splittedCode.Length; j++)
            {
                string[] strList = splittedCode[j].Split(' ');
                for (int i = 0; i < strList.Length; i++)
                {
                    if (Splitters.ignoreStrings.Contains(strList[i])) { continue; }

                    string currentToken = filterString(strList[i]).ToLower();
                    if (TokensDictionary.inputToTokenEnum.ContainsKey(currentToken))
                    {
                        tokens.Add(currentToken);
                        continue;
                    }

                    List<string> complexTokens = formatComplexTokens(currentToken);

                    for (int k = 0; k < complexTokens.Count; k++)
                    {
                        if (Splitters.ignoreStrings.Contains(complexTokens[k])) { continue; }
                        tokens.Add(filterString(complexTokens[k]));
                    }
                }
            }
            
            return filterTokens(tokens);
        }

    }


    public class TokensIdentifier
    {
        static Regex NumberRegex = new Regex(@"^[0-9]+(\.[0-9]*)?$");
        static Regex IdentifierRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");

        public List<TokenEnums> identifyTokens(List<string> tokens)
        {
            List<TokenEnums> output = new List<TokenEnums>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (isReservedWord(tokens[i]))
                {
                    output.Add(TokensDictionary.inputToTokenEnum[tokens[i]]);
                    continue;
                }
                if (isConstantNumber(tokens[i]))
                {
                    output.Add(TokenEnums.constantNumber);
                    continue;
                }
                if (isIdentifier(tokens[i]))
                {
                    output.Add(TokenEnums.identifier);
                    continue;
                }

                output.Add(TokenEnums.error);
            }
            return output;
        }

        public static bool isReservedWord(string token)
        {
            return (TokensDictionary.inputToTokenEnum.ContainsKey(token));
        }

        public static bool isConstantNumber(string token)
        {
            return (NumberRegex.IsMatch(token));
        }

        public static bool isIdentifier(string token)
        {
            return IdentifierRegex.IsMatch(token);
        }
    }
}
