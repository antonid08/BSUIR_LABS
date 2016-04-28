class MyDefaultDict(dict):
    def __init__(self, defaultFactory=None):
        self.defaultFactory = defaultFactory

    def __missing__(self, key):
        if self.defaultFactory is None:
            raise KeyError(key)
        self[key] = MyDefaultDict(self.defaultFactory)
        return self[key]

    def __getitem__(self, key):
        if key not in self.iterkeys():
            MyDefaultDict.__missing__(self, key)
        return super(MyDefaultDict, self).__getitem__(key)


def main():
    test = MyDefaultDict(int)
    test[2][1] = 0
    print test[3]
    print test[2][3]
    print test
    # test[2][3] = '2'
    # s = kek['s']
    # print dir(dict)

if __name__ == '__main__':
    main()
