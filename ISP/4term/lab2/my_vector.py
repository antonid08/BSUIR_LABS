class MyVector(object):
    def __init__(self, elems=[]):
        if isinstance(elems, (list, tuple)):
            self.mainList = elems
        else:
            raise TypeError

    def add(self, value):
        if isinstance(value, (list, tuple)):
            self.mainList += value
        elif isinstance(value, (int, float)):
            self.mainList.append(value)
        else:
            raise TypeError

    def __add__(self, other):
        try:
            return MyVector(map(lambda x, y: x + y, self.mainList,
                                other.mainList))
        except TypeError:
            raise ValueError('Vectors have to be identical dimension')

    def __sub__(self, other):
        try:
            return MyVector(map(lambda x, y: x - y, self.mainList,
                                other.mainList))
        except TypeError:
            raise ValueError('Vectors have to be identical dimension')

    def __mul__(self, other):
        if isinstance(other, (int, float)):
            return MyVector([x * other for x in self.mainList])
        elif isinstance(other, MyVector):
            try:
                return sum(map(lambda x, y: x * y, self.mainList,
                               other.mainList))
            except TypeError:
                raise ValueError('Vectors have to be identical dimension')
        raise TypeError

    def __eq__(self, other):
        try:
            return (True if self.mainList == other.mainList else False)
        except Exception:
            raise TypeError

    def __getitem__(self, index):
        return self.mainList[index]

    def size(self):
        return len(self.mainList)

    def __str__(self):
        return ' '.join(str(elem) for elem in self.mainList)


def main():
    v1 = MyVector([1, 2, 3])
    v2 = MyVector([0, 0, 0])

    print v1 * v2

    print str(v1.size())

if __name__ == '__main__':
    main()
