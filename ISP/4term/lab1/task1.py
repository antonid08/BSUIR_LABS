import re


def getWords(text):
    text = text.replace('.', ' ')
    text = text.replace('?', ' ')
    text = text.replace('!', ' ')
    text = text.replace(',', ' ')
    listOfWords = text.split()

    return listOfWords


def getSentences(text):

    sentences = re.split('\.|!|\?', text)

    for sentence in sentences:
        if not sentence:
            sentences.remove(sentence)

    return sentences


def getWordsDict(text):
    listOfWords = getWords(text)
    dictOfWords = {}
    for word in listOfWords:
        if dictOfWords.has_key(word):
            dictOfWords[word] += 1
        else:
            dictOfWords.update({word: 1})

    return dictOfWords


def getAverageNumberOfWordsInTheSentence(text):
    numberOfSentences = 0
    average = 0.0

    numberOfWords = len(getWords(text))

    numberOfSentences = text.count('. ') + text.count('? ') + text.count('! ')

    if text[-1] == '.' or text[-1] == '!' or text[-1] == '?':
        numberOfSentences += 1

    try:
        average = float(numberOfWords) / float(numberOfSentences)
    except ZeroDivisionError:
        average = numberOfWords

    return average


def getMedianNumberOfWordsInTheSentece(text):
    sentences = getSentences(text)
    wordsInSentences = []

    print sentences

    for sentence in sentences:
        wordsInSentences.append(len(getWords(sentence)))

    print wordsInSentences

    wordsInSentences.sort()
    return wordsInSentences[len(wordsInSentences) / 2]


def sortDict(unsortedDict):
    items = unsortedDict.items()
    items.sort(key=lambda item: item[1], reverse=1)

    return items


def getTopK(text, k, n):
    words = getWords(text)
    ngramms = {}
    for word in words:
        endNgramm = n

        wordLen = len(word)
        while endNgramm <= wordLen:
            ngramm = word[endNgramm - n:endNgramm]
            if ngramms.has_key(ngramm):
                ngramms[ngramm] += 1
            else:
                ngramms.update({ngramm: 1})
            endNgramm += 1

    sortedNgramms = sortDict(ngramms)

    counter = 0
    while counter < k:
        print sortedNgramms[counter][0] + ' - %d' % sortedNgramms[counter][1]
        counter += 1


text = raw_input('Ill find top-K N-gramms in line. Input the line.\n')
k = input('K: ')
n = input('N: ')

getTopK(text, k, n)
