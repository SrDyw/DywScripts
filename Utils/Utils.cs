using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DywFunctions {
    namespace Utils {
        public interface ISortElement {
            int Value();
        }
        public interface IPrintable<T> {
            T PrintableAtbr();
        }
        public enum OrdenBy {
            Ascendent,
            Descendent
        }
        public static class Utils {
            public static void MergeSort(ISortElement[] collection, OrdenBy ordenBy = OrdenBy.Ascendent) {
                MergeSortInRange(collection, 0, collection.Length - 1, ordenBy);
            }
            static void MergeSortInRange(ISortElement[] collection, int left, int right, OrdenBy ordenBy) {
                if (left < right) {
                    int middle = (left + right) / 2;

                    MergeSortInRange(collection, left, middle, ordenBy);
                    MergeSortInRange(collection, middle + 1, right, ordenBy);


                    Merge(collection, left, middle, right, ordenBy);
                }
            }

            static void Merge(ISortElement[] arr, int l, int m, int r, OrdenBy ordenBy) {
                int n1 = m - l + 1;
                int n2 = r - m;
        
                ISortElement[] L = new ISortElement[n1];
                ISortElement[] R = new ISortElement[n2];
        
                for (int i = 0; i < n1; ++i)
                    L[i] = arr[l + i];
                for (int j = 0; j < n2; ++j)
                    R[j] = arr[m + 1 + j];
        
                int k = l;
                int p = 0, q = 0;
                while (p < n1 && q < n2)
                {
                    bool orderLogic = ordenBy != OrdenBy.Ascendent ? L[p].Value() >= R[q].Value() : L[p].Value() <= R[q].Value();

                    if (orderLogic)
                    {
                        arr[k] = L[p];
                        p++;
                    }
                    else
                    {
                        arr[k] = R[q];
                        q++;
                    }
                    k++;
                }
        
                while (p < n1)
                {
                    arr[k] = L[p];
                    p++;
                    k++;
                }
        
                while (q < n2)
                {
                    arr[k] = R[q];
                    q++;
                    k++;
                }
            }
            public static void PrintArray<T>(T[] arr, string separator = " ")
            {
                int n = arr.Length;
                string msg = "Collection is Empty";

                for (int i = 0; i < n; ++i) {
                    if (msg == "Collection is Empty") msg = "";

                    msg += arr[i] + separator;
                }

                Debug.Log(msg);
            }

            public static void PrintCollection<T>(IPrintable<T>[] collection, string separator = " ") {
                string msg = "Empty";
                for(int i = 0; i < collection.Length; i++) {
                    if (msg == "Empty") msg = "";

                    msg += collection[i].PrintableAtbr() + separator;
                }

                Debug.Log(msg);
            }

            public static void QuickSort(int[] arr, int begin, int end) {
                if (begin < end) {
                    int partitionIndex = Partition(arr, begin, end);

                    QuickSort(arr, begin, partitionIndex-1);
                    QuickSort(arr, partitionIndex+1, end);
                }
            }

            static int Partition(int[] arr, int begin, int end) {
                int pivot = arr[end];
                int i = (begin-1);

                for (int j = begin; j < end; j++) {
                    if (arr[j] <= pivot) {
                        i++;

                        int swapTemp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = swapTemp;
                    }
                }

                int swapTemp1 = arr[i+1];
                arr[i+1] = arr[end];
                arr[end] = swapTemp1;

                return i+1;
            }
            ///<summary>
            ///<returrn>Returns a array with the elements that match the generic</returns>
            ///</summary>
            public static T[] GetComponentsOfType<T>(this Component[] components) {
                List<T> result = new List<T>();
                
                foreach (var component in components)
                {
                    T typedComponent = component.GetComponent<T>();
                    if (typedComponent != null && !result.Contains(typedComponent)) {
                        result.Add(typedComponent);
                    }
                }
                return result.ToArray();
            }

            ///<summary>
            ///<params name="type">The element that will be added to result</params>
            ///<returrn>Returns a array with the elements that match the params</returns>
            ///</summary>
            public static Component[] GetComponentsOfType(this Component[] components, params Type[] types) {
                List<Component> result = new List<Component>();
                
                foreach (var component in components)
                {
                    foreach (var type in types)
                    {
                        if (component.GetType() == type && !result.Contains(component)) {
                            result.Add(component);
                        }
                    }
                    
                }
                return result.ToArray();
            }


            public static Component[] FindInRaycast(this RaycastHit[] raycasts, Type type) {
                List<Component> result = new List<Component>();

                foreach (var ray in raycasts)
                {
                    Collider col = ray.collider;
                    Component[] components = col.GetComponents<Component>();
                    foreach (var component in components)
                    {
                        if (component.GetType() == type && !result.Contains(component)) {
                            result.Add(component);
                        }
                    }
                    
                }

                return result.ToArray();
            }
        }
    }
}