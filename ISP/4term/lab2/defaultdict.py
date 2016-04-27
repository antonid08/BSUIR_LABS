class MyDefaultDict(dict):
    def __init__(self, defaultFactory=None, *args, **kwargs):
        self.defaultFactory = defaultFactory
        super(MyDefaultDict, self).__init__(*args, **kwargs)

    def __missing__(self, key):
        if self.defaultFactory is None:
            raise KeyError(key)
        super(MyDefaultDict, self).__setitem__(key, self.defaultFactory())

    def __getitem__(self, key):
        print 'hey! i am here!'
        if key not in self.iterkeys():
            MyDefaultDict.__missing__(self, key)
        return super(MyDefaultDict, self).__getitem__(key)


def main():
    test = MyDefaultDict(str)
    test[3] += '2'

    test['2'] = 2
    # s = kek['s']
    print test
    # print dir(dict)

if __name__ == '__main__':
    main()
