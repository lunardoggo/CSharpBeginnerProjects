# Landau Notation
Landau notation (or big O notation) describes describes the growth of a particular function. In computer science it's often used to analyze and classify algorithms according to their so called "time complexity" and "space complexity".

# Symbols
The following symbols are used to denote the growth of a function **f**:
 - **O(g(n))** (big O): means that the function **f** grows as fast or slower than **g** for big **n**
 - **Θ(g(n))** (big Theta): means that the function **f** grows exactly as fast as **g** for big **n**
 - **Ω(g(n))** (big Omega): means that the function **f** grows at least as fast as **g** for big **n**
 - **o(g(n))** (small O): means that the function **f** grows slower than **g** for big **n**
 - **ω(g(n))** (small omega): means that the function **f** grows faster than **g** for big **n**
 
Big O is most commonly used in practice, but Θ and Ω also occur sometimes. The other two (o, ω) are used quite rarely in algorithm analysis. Some important things to note are that:
1. all of these notations only make sense if **n** approaches infinity, as, for example, the time difference between the execution of two algorithms is almost zero for a small dataset in practice and there may be cases where **O(f(n)) > O(g(n))**, but **f(n) < g(n)** for small **n** values
2. there is a certain order of which function grows faster as another one, such as **O(1) < O(n) < O(n * log(n)) < O(n^2) < O(2^n)**
3. an algorithm is most commonly considered efficient if it has a polynomial worst case time complexity. The biggest exponent of the function is irrelevant for that definition, **O(n)** is efficient, as well as **O(n^10000000)**
4. there are rules for combinding multiple big **O** (or one of the other notations, as long as all symbols match) notations into one:
   - multiplicative constants can be ignored: **O(n) = O(2n)**
   - **O(a) + O(b) = O(a + b)**, if **O(a) > O(b)**, **O(b)** can be omitted, if **O(a) < O(b)**, **O(a)** can be omitted, if **O(a) = O(b)**, either one can be omitted. You can see this rule in action when using graphs, as a time complexity of **O(|V| + |E|)** could be reduced to **O(|V|^2)**, as **O(|E|) < O(|V|^2)** in general, but there are graphs where **O(|E|) = O(|V|)**, so it's sometimes better to keep both around to make the upper bound more strict
   - **O(a) * O(b) = O(a * b)**, this rule is for example used when analyzing loops. If the body of a loop has a time complexity of **O(a)** and the loop runs **O(b)** times, there are **O(b)** occurrences of **O(a)** in the analysis, so you would multiply them
5. You can analyze the best- average- and worst-case time/space complexity separately, for example **InsertionSort** has a best case time complexity of **O(n)** where **n** is the amount of items in the sorted array, but the average- and worst-case time complexity is **O(n^2)**

Concrete examples and further clarification for points **1.** and **2.** can be found on [libretexts.org](https://eng.libretexts.org/Bookshelves/Computer_Science/Programming_Languages/Think_Python_-_How_to_Think_Like_a_Computer_Scientist_(Downey)/13%3A_Appendix_B-_Analysis_of_Algorithms/13.01%3A_Order_of_Growth)