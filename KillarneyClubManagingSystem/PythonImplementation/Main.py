from kcmsconfigconverter import KCMSConfig
from core import KCMS

Handler = None


def main():
    global Handler
    print('Welcome to Killarney Club Management System (KCMS).')
    print('Please load the configuration file:')
    i = input('(default data.ini) >')
    print('What is the format of the configuration file?')
    print('1. KCMSFormat 2. JSONFormat')
    f = '2' if input('(default 1) >') == '2' else '1'
    if f == '1' and i:
        print('Trying to load', i, 'as KCMSConfig Format...')
        Handler = KCMS(i)
    elif f == '1' and not i:
        print('Trying to load default file as KCMSConfig Format...')
        Handler = KCMS()
    elif f == '2':
        if not i:
            i = 'data.ini'
        print(f'Trying to load {i} as JSON Format...')
        with open(i) as g:
            Handler = KCMS(
                kcmsConvertedDict=KCMSConfig.IO.JSON.fromJson(g.read()))

    print('Successfully loaded configuration file.')
    loop()


def OutputKCMSConfig():
    fn = input('Filename:')
    try:
        with open(fn, 'w') as f:
            f.write(Handler.SaveConfig())
        print('Finished.')
    except:
        print('Unable to save file.')

    loop()


def OutputJSON():
    fn = input('Filename:')
    try:
        with open(fn, 'w') as f:
            f.write(KCMSConfig.IO.JSON.toJson(Handler.SaveConfig()))
        print('Finished.')
    except:
        print('Unable to save file.')

    loop()


def ExportFinal():
    fn = input('Filename:')
    try:
        Handler.ExportFinalData(fn)
    except:
        print('Unable to complete the command')
        raise

    loop()


def ExportEvent():
    Events = Handler.GetAllEvents()
    for x in range(len(Events)):
        print(x+1, Events[x], sep='.')
    i = int(input('>'))
    if i > len(Events) or i <= 0:
        print('Invalid selection.')
    else:
        fn = input('Filename:')
        try:
            with open(fn, 'w') as f:
                Columns = []
                Columns.append(
                    (Events[i-1]+' Result', Handler.GetEventResultsByEventName(Events[i-1])))
                Columns.append(
                    (Events[i-1]+' Placing', Handler.GetPlacingByEventName(Events[i-1])))
                Columns.append(
                    (Events[i-1]+' Point', Handler.GetPointsByEventName(Events[i-1])))
                f.write(Handler.ConvertToCSV(Columns,  Handler.SortEvent(Events[i-1])))
        except:
            print('Failed to save results.')
            raise
    loop()


def loop():
    print('Choose from the option below:')
    print('1. Convert current file to KCMSConfig Format')
    print('2. Convert current file to JSON Format')
    print('3. Export final placing and scores')
    print('4. Export an event...')
    # print('5. Export an athlete...')
    i = input('>')
    while i not in FunctionMap:
        print('Invalid option.')
        i = input('>')
    FunctionMap[i]()


FunctionMap = {
    '1': OutputKCMSConfig,
    '2': OutputJSON,
    '3': ExportFinal,
    '4': ExportEvent
}


if __name__ == '__main__':
    main()
