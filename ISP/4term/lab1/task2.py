def myQuickSort(unsorted):
    if unsorted:
        return (myQuickSort([elem for elem in unsorted if elem < unsorted[0]]) +
                [elem for elem in unsorted if elem == unsorted[0]] +
                myQuickSort([elem for elem in unsorted if elem > unsorted[0]]))
    return []


def mergeSort(unsorted):
    def merge(left, rigth):
        result = []
        while min([len(left), len(rigth)]):
            result.append((left if left[0] < rigth[0] else rigth).pop(0))
        return result + left + rigth

    if len(unsorted) != 1:
        return merge(mergeSort(unsorted[:len(unsorted) / 2]),
                     mergeSort(unsorted[len(unsorted) / 2:]))
    return unsorted


def radixSort(unsorted):
    length = len(str(max(unsorted)))
    rang = 10
    for counter in range(length):
        tempList = [[] for k in range(rang)]
        for digit in unsorted:
            figure = digit // 10**counter % 10
            tempList[figure].append(digit)
        unsorted = []
        for k in range(rang):
            unsorted += tempList[k]
    return unsorted

if __name__ == '__main__':
    data = raw_input('Input numbers: ')
    unsortedList = [int(element) for element in data.split(' ')]
    print radixSort(unsortedList)
