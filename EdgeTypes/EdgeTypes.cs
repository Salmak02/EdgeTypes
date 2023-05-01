using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class EdgeTypes
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Detect & count the edge types of the given UNDIRECTED graph by applying COMPLETE-DFS on the entire graph 
        /// NOTE: during search, break ties (if any) by selecting the verices in ASCENDING numeric order
        /// </summary>
        /// <param name="vertices">array of vertices in the graph (named from 0 to |V| - 1)</param>
        /// <param name="edges">array of edges in the graph</param>
        /// <returns>return array of 3 numbers: outputs[0] number of backward edges, outputs[1] number of forward edges, outputs[2] number of cross edges</returns>
        static public int[] DetectEdges(int[] vertices, KeyValuePair<int, int>[] edges)
        {

            int sz = vertices.Length;
            var adj_vertex = new Dictionary<int, List<int>>();
            Dictionary<int, int> val_ind = new Dictionary<int, int>();
            for (int i = 0; i < sz; i++) val_ind[vertices[i]] = i;
            int[] edge_types = new int[3], 
                color = new int[sz];
            //0 for white -> not visited
            foreach (var edge in edges)
            {
                if (!adj_vertex.ContainsKey(edge.Key)) adj_vertex[edge.Key] = new List<int>();
                adj_vertex[edge.Key].Add(edge.Value);//adj of each vertex with elminating loops
                if (!adj_vertex.ContainsKey(edge.Value)) adj_vertex[edge.Value] = new List<int>();
                adj_vertex[edge.Value].Add(edge.Key);
            }
            for (int i = 0; i < sz; i++)
            {
                if (color[i] == 0) Dfs(i);
            }
            void Dfs(int index)
            {
                //Console.WriteLine(index);
                color[index] = 1;//1 for gray
                //time++;
                //disc_T[index] = time;
                int val = vertices[index];
                if (adj_vertex.ContainsKey(val))
                {
                    foreach (int vertex in adj_vertex[val])   //vertex iterator
                    {
                        int ind = val_ind[vertex]; //current index
                        if (color[ind] == 0)
                        {
                            //parent[ind] = vertices[index];
                            Dfs(ind);
                        }
                        else if (color[ind] == 2 )
                        {//backward edge are considered forward and vice versa
                            edge_types[1]++;edge_types[0]++;
                        }
                        
                    }
                }
                color[index] = 2;//black i.e explored
               // time++;
                //fin_T[index] = time;

            }
            return edge_types;
        }




        #endregion
    }
}
