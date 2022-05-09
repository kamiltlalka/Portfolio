#include <iostream>
#include <string>
#include <fstream>

using namespace std;

template <typename Key, typename Info>
class Dictionary {
private:




    struct Node
    {
        Key key;
        Info info;
        Node* Left;
        Node* Right;

        Node() :Left(nullptr), Right(nullptr) {}
        Node(Key _key, Info _info) :key(_key), info(_info), Left(nullptr), Right(nullptr) {}
        

    };
    Node* Root;


    void print(Node* newroot) {
        if (newroot)
        {
            
            
            print(newroot->Right);
            cout << "Key: " << newroot->key << "   Info: " << newroot->info << endl;
            print(newroot->Left);
        }

    }

    void printgraph(const std::string& prefix, const Node* newroot, bool isLeft)
    {
        if (newroot != nullptr)
        {
            std::cout << prefix;
            std::cout << (isLeft ? "|--" : "L--");
            cout << "Key: " << newroot->key << "   Info: " << newroot->info << endl;
            printgraph(prefix + (isLeft ? "|   " : "    "), newroot->Right, true);
            printgraph(prefix + (isLeft ? "|   " : "    "), newroot->Left, false);
        }
    }

    int getHeight(Node* node) {

        if (node == NULL)
            return 0;
        else
        {
            int left_side;
            int right_side;
            left_side = getHeight(node->Left);
            right_side = getHeight(node->Right);
            if (left_side > right_side)
            {
                return left_side + 1;

            }
            else
                return right_side + 1;
        }

    }


    Node* rr_rotat(Node* parent) {
        Node* t;
        t = parent->Right;
        parent->Right = t->Left;
        t->Left = parent;
        return t;
    }
    Node* ll_rotat(Node* parent) {
        Node* t;
        t = parent->Left;
        parent->Left = t->Right;
        t->Right = parent;
        return t;
    }
    Node* lr_rotat(Node* parent) {
        Node* t;
        t = parent->Left;
        parent->Left = rr_rotat(t);
        return ll_rotat(parent);
    }
    Node* rl_rotat(Node* parent) {
        Node* t;
        t = parent->Right;
        parent->Right = ll_rotat(t);
        return rr_rotat(parent);
    }
    Node* balance(Node* t) {
        int bal_factor = getBalance(t);
        if (bal_factor > 1) {
            if (getBalance(t->Left) > 0)
                t = ll_rotat(t);
            else
                t = lr_rotat(t);
        }
        else if (bal_factor < -1) {
            if (getBalance(t->Right) > 0)
                t = rl_rotat(t);
            else
                t = rr_rotat(t);
        }
        return t;
    }


    int getBalance(Node* newroot)
    {

        if (newroot == NULL)
            return 0;
        else
        {
            int left_side;
            int right_side;
            left_side = getHeight(newroot->Left);
            right_side = getHeight(newroot->Right);
            return left_side - right_side;
        }

    }

    bool removeKey(Key key, int occurence, Node* newroot) {

        if (newroot == nullptr)
            return false;

        if (newroot->key > key)
        {
            if (newroot->Right)
            {
                if (newroot->Right->key == key)
                {
                    if (occurence == 1)
                    {
                        Node* temp = newroot->Right;
                        if (newroot->Right->Right == nullptr && newroot->Right->Left == nullptr)
                        {
                            newroot->Right = nullptr;
                            delete(temp);
                            Root = balance(Root);
                            return true;
                        }

                        Node* farleft = newroot->Right->Right;
                        if (farleft == nullptr)
                        {
                            newroot->Right = newroot->Right->Left;
                            delete(temp);
                            Root = balance(Root);
                            return true;
                        }

                        Node* prefarleft = newroot->Right;
                        while (farleft->Left)
                        {
                            prefarleft = farleft;
                            farleft = farleft->Left;
                        }
                        if (prefarleft == newroot->Right)
                        {
                            newroot->Right = farleft;
                            newroot->Right->Left = temp->Left;
                            delete(temp);
                            Root = balance(Root);
                            return true;

                        }

                        newroot->Right = farleft;
                        prefarleft->Left = farleft->Right;
                        newroot->Right->Right = temp->Right;
                        newroot->Right->Left = temp->Left;
                        delete(temp);
                        Root = balance(Root);
                        return true;
                    }
                    else
                    {
                        if (removeKey(key, occurence - 1, newroot->Right) || removeKey(key, occurence - 1, newroot->Left))
                            return true;
                        
                        return false;
                    }

                }
                else
                {
                    if (removeKey(key, occurence, newroot->Right) || removeKey(key, occurence, newroot->Left))
                        return true;

                    return false;


                }
            }
            else
                return false;
        }
        else
        {
            if (newroot->Left)
            {
                if (newroot->Left->key == key)
                {
                    if (occurence == 1)
                    {
                        Node* temp = newroot->Left;
                        if (newroot->Left->Right == nullptr && newroot->Left->Left == nullptr)
                        {
                            newroot->Left = nullptr;
                            delete(temp);
                            Root = balance(Root);
                            return true;
                        }

                        Node* farleft = newroot->Left->Right;
                        if (farleft == nullptr)
                        {
                            newroot->Left = newroot->Left->Left;
                            delete(temp);
                            Root = balance(Root);
                            return true;
                        }

                        Node* prefarleft = newroot->Left;
                        while (farleft->Left)
                        {
                            prefarleft = farleft;
                            farleft = farleft->Left;
                        }
                        if (prefarleft == newroot->Left)
                        {
                            newroot->Left = farleft;
                            newroot->Left->Left = temp->Left;
                            delete(temp);
                            Root = balance(Root);
                            return true;

                        }

                        newroot->Left = farleft;
                        prefarleft->Left = farleft->Right;
                        newroot->Left->Right = temp->Right;
                        newroot->Left->Left = temp->Left;
                        delete(temp);
                        Root = balance(Root);
                        return true;
                    }
                    else
                    {

                        if (removeKey(key, (occurence - 1), newroot->Right) || removeKey(key, (occurence - 1), newroot->Left))
                            return true;

                        return false;
                    }
                }
                else
                {
                    if (removeKey(key, occurence, newroot->Right) || removeKey(key, occurence, newroot->Left))
                        return true;

                    return false;


                }


            }
            else
                return false;
        }



        return false;
    }

    Info getElement(Key key, int occurence, Node* newroot)
    {
        if (newroot == nullptr)
            return {};

        if (newroot->key > key)
        {
            if (newroot->Right)
            {
                if (newroot->Right->key == key)
                {
                    if (occurence == 1)
                    {
                        return newroot->Right->info;
                    }
                    else
                    {
                        return getElement(key, occurence - 1, newroot->Right) + getElement(key, occurence - 1, newroot->Left);

                    }

                }
                else
                {
                    return getElement(key, occurence, newroot->Right) + getElement(key, occurence, newroot->Left);


                }
            }
            else
                return {};
        }
        else
        {
            if (newroot->Left)
            {
                if (newroot->Left->key == key)
                {
                    if (occurence == 1)
                    {
                        return newroot->Left->info;
                    }
                    else
                    {
                        return getElement(key, occurence - 1, newroot->Right) + getElement(key, occurence - 1, newroot->Left);

                    }

                }
                else
                {
                    return getElement(key, occurence, newroot->Right) + getElement(key, occurence, newroot->Left);


                }
            }
            else
                return {};
        }



        return {};
    }

    bool alter(Key key, Info newInfo, int occurence, Node* newroot) {
        if (newroot == nullptr)
            return false;

        if (newroot->key > key)
        {
            if (newroot->Right)
            {
                if (newroot->Right->key == key)
                {
                    if (occurence == 1)
                    {
                        newroot->Right->info = newInfo;
                        return true;
                    }
                    else
                    {
                        return alter(key, newInfo, occurence - 1, newroot->Right) || alter(key, newInfo, occurence - 1, newroot->Left);

                    }

                }
                else
                {
                    return alter(key, newInfo, occurence, newroot->Right) || alter(key, newInfo, occurence, newroot->Left);


                }
            }
            else
                return false;
        }
        else
        {
            if (newroot->Left)
            {
                if (newroot->Left->key == key)
                {
                    if (occurence == 1)
                    {
                        newroot->Left->info = newInfo;
                        return true;
                    }
                    else
                    {
                        return alter(key, newInfo, occurence - 1, newroot->Right) || alter(key, newInfo, occurence - 1, newroot->Left);

                    }

                }
                else
                {
                    return alter(key, newInfo, occurence, newroot->Right) || alter(key, newInfo, occurence, newroot->Left);


                }
            }
            else
                return false;
        }

    }


public:


    Dictionary() : Root(nullptr) {}
    Dictionary(const Dictionary<Key, Info>& dict)
    {
        Root = dict.Root;
    }
   

    bool add(Key key, Info info) {

        if (!Root)
        {
            Root = new Node(key, info);
            return true;
        }
        else
        {
            Node* temp = Root;
            Node* pretemp = Root;
            while (temp)
            {
                if (temp->key > key)
                {
                    if (temp->Right)
                    {
                        pretemp = temp;
                        temp = temp->Right;
                    }
                    else
                    {
                        temp->Right = new Node(key, info);
                        if (pretemp->Right == temp)
                            pretemp->Right = balance(temp);
                        else if (pretemp->Left == temp)
                            pretemp->Left = balance(temp);

                        Root->Left = balance(Root->Left);
                        Root->Right = balance(Root->Right);
                        Root = balance(Root);

                        return true;
                    }
                }
                else
                {
                    if (temp->Left)
                    {
                        pretemp = temp;
                        temp = temp->Left;
                    }
                    else
                    {
                        temp->Left = new Node(key, info);
                        if (pretemp->Right == temp)
                            pretemp->Right = balance(temp);
                        else if (pretemp->Left == temp)
                            pretemp->Left = balance(temp);

                        Root->Left = balance(Root->Left);
                        Root->Right = balance(Root->Right);
                        Root = balance(Root);

                        return true;
                    }

                }
            }

        }



        return false;
    }

    bool removeKey(Key key, int occurence) {

        if (!Root)
        {
            return false;
        }
        else
        {
            if (Root->key == key)
            {
                if (occurence == 1)
                {
                    Node* temp = Root;
                    if (Root->Right == nullptr && Root->Left == nullptr)
                    {
                        Root = nullptr;
                        delete(temp);

                        return true;
                    }

                    Node* farleft = Root->Right;
                    if (farleft == nullptr)
                    {
                        Root = Root->Left;
                        delete(temp);
                        Root = balance(Root);
                        return true;
                    }

                    Node* prefarleft = Root;
                    while (farleft->Left)
                    {
                        prefarleft = farleft;
                        farleft = farleft->Left;
                    }
                    if (prefarleft == Root)
                    {
                        Root = farleft;
                        Root->Left = temp->Left;
                        delete(temp);
                        Root = balance(Root);
                        return true;

                    }

                    Root = farleft;
                    prefarleft->Left = farleft->Right;
                    Root->Right = temp->Right;
                    Root->Left = temp->Left;
                    delete(temp);
                    Root = balance(Root);
                    return true;
                }
                else
                {
                    return removeKey(key, occurence - 1, Root);
                }
            }
            else
            {
                return removeKey(key, occurence, Root);
            }
        }
        return false;
    }

    int removeEveryKey(Key key)
    {
        int counter = -1;
        bool tracker = true;
        while (tracker)
        {
            tracker = removeKey(key, 1);
            counter++;
        }


        return counter;
    }

    int getBalance()
    {

        if (Root == NULL)
            return 0;
        else
        {
            int left_side;
            int right_side;
            left_side = getHeight(Root->Left);
            right_side = getHeight(Root->Right);
            return left_side - right_side;
        }

    }

    bool alter(Key key, Info newInfo, int occurence) {
        if (!Root)
        {
            return false;
        }
        else
        {
            if (Root->key == key)
            {
                if (occurence == 1)
                {
                    Root->info = newInfo;
                    return true;
                }
            }
            else
            {
                return alter(key, newInfo, occurence, Root);
            }
        }
        return false;

    }

    void print() {
        cout << "---------------------------------------" << endl;
        print(Root->Left);
        cout << "Key: " << Root->key << "   Info: " << Root->info << endl;
        print(Root->Right);
        cout << "---------------------------------------" << endl;
    }

    Info getElement(Key key, int occurence) {

        if (!Root)
        {
            return {};
        }
        else
        {
            if (Root->key == key)
            {
                if (occurence == 1)
                {
                    return Root->info;
                }
                else
                {
                    return getElement(key, occurence-1, Root);
                }
            }
            else
            {
                return getElement(key, occurence, Root);
            }
        }
        return {};





        return {};
    }

    void printgraph()
    {
        printgraph("", Root, false);
    }


};


void testOfTree()
{
    Dictionary<int, string> lectures;


    lectures.add(20, "banan");
    lectures.add(10, " ");
    lectures.add(30, "");
    lectures.add(15, "");
    lectures.add(55, "japko");
    lectures.add(44, "japko");
    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.getBalance() << endl;
    cout << "----------------------------" << endl;
    lectures.add(44, "japko");
    lectures.add(44, "niebaban");
    lectures.add(44, "japko");
    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.getBalance() << endl;
    cout << "----------------------------" << endl;
    lectures.add(33, "japko");
    lectures.add(35, "");
    lectures.add(25, "orange");

    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.getBalance() << endl;
    cout << "----------------------------" << endl;

    lectures.printgraph();
    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.removeEveryKey(44) << endl;
    cout << "----------------------------" << endl;
    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.getBalance() << endl;
    cout << "----------------------------" << endl;
    lectures.printgraph();
    lectures.removeKey(30, 1);
    lectures.removeKey(20, 1);
    lectures.removeKey(35, 1);

    cout << "----------------------------" << endl;
    cout << "balance:" << endl;
    cout << lectures.getBalance() << endl;
    cout << "----------------------------" << endl;
    lectures.printgraph();

}

bool inWord(string& word, ifstream& inFile) {
    //-------------------------
    //
    // Return a word extracted from an input file
    //
    bool isWord(false);
    char c;
    if (inFile.is_open()) {
        while (!inFile.eof()) { // first look for a word starting character
            inFile.get(c);
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) {
                isWord = true;
                word = c;    // assign the word starting character
                c = '\0';
                break;
            }
        }
        while (!inFile.eof()) { // then append remaining (following) characters to the word
            inFile.get(c);
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) {
                word += c;   // append the character to the word
                c = '\0';
            }
            else {
                break;
            }
        }
    }
    return isWord;
}

Dictionary<string,int>* counter(const string& fileName) {


    Dictionary<string, int> *outputtree = new Dictionary<string, int>;
    int infovalue = 0;

    ifstream inFile(fileName);
    string  word;
    int wordCount(0);
    if (inFile.is_open()) {
        cout << "Successfully opened file '" << fileName << "'" << endl;
        while (inWord(word, inFile)) {

            infovalue = outputtree->getElement(word,1);

            if (infovalue > 0)
            {
                outputtree->alter(word, (infovalue+1),1);
            }
            else
            {
                outputtree->add(word, 1);
            }
            cout << "{" << word << "}";
        };
    }
    else {
        cout << "Can not open file '" << fileName << "'" << endl;
    }

    return outputtree;
}

int main() {

  

}