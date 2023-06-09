using System;


namespace DywFunctions 
{   
    namespace DataList
    {
        [System.Serializable]
        public class DataList <Template>
        {
            Template[] objectList;
            int count;

            public int Count { get => count; private set => count = value;}


            public DataList(int size) {
                objectList = new Template[size];
                Count = 0;
            }

            public void AddItem(Template item) {
                if (count < objectList.Length)
                    objectList[count++] = item;
                else {
                    throw new Exception(message: "Full list!");
                }
            }

            public void PopItem(int index) {
                if (index >= 0 & index < Count) {
                    objectList[index] = default;

                    for(int i = index; i < Count - 1; i++) {
                        objectList[i] = objectList[i + 1];
                    }
                    objectList[Count - 1] = default;

                    Count--;
                } else {
                    throw new Exception(message: "Index out of bounds!");
                }
            }

            public Template ItemIn(int index) {
                Template objectInIndex = default;

                if (index >= 0 & index < Count) {
                    objectInIndex = objectList[index];
                }
                else 
                    throw new Exception(message: "Index out of bounds!");

                return objectInIndex;
            }

            public int IndexOf(Template template) {
                int index = -1;
                int iteratorIndex = 0;

                foreach (var i in objectList)
                    {
                        if (i.Equals(template)) {
                            index = iteratorIndex;
                            break;
                        }

                        iteratorIndex++;
                    }

                return index;
            }

            public bool ContainItem(Template item) {
                bool itemExists = false;

                try {
                    foreach (var i in objectList)
                    {
                        if (i.Equals(item)) {
                            itemExists = true;
                            break;
                        }
                    }
                } catch  {
                    itemExists = false;
                }

                return itemExists;
            }

            public void RemplaceInPosition(int index, Template item) {
                if (index >= 0 & index < objectList.Length) {
                    objectList[index] = item;
                }
                else 
                    throw new Exception(message: "Index out of bounds!");
            }

            public Template[] Items() {
                return objectList;
            }

            public int GetTrueSize() {
                return objectList.Length;
            }
        }

    }
}
