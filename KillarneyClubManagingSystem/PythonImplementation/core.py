from kcmsconfigconverter import KCMSConfig


class KCMS:

    def __init__(self, configLocation='data.ini', kcmsConvertedDict=None):
        super().__init__()
        result, exc = self.LoadConfig(configLocation, kcmsConvertedDict)
        if not result:
            raise Exception('Failed to load configuration file:'+str(exc))

    def LoadConfig(self, fileName='data.ini', kcmsConvertedDict=None):
        # Load config
        if not kcmsConvertedDict:
            try:
                with open(fileName) as f:
                    self.Config = KCMSConfig.IO.Input.toDict(f.read())
                    return True, None
            except Exception as e:
                return False, e
        else:
            self.Config = kcmsConvertedDict
            return True, None

    def GetEventResultsByEventName(self, EventName):
        if EventName not in self.GetAllEvents():
            return {}
        else:
            atheDatas = self.Config['Data']['Athlete']
            eventData = {}
            for atheData in atheDatas:
                d = KCMSConfig.MappedDataCompatiable(atheDatas[atheData])
                if EventName in d:
                    if d[EventName] == '':
                        d[EventName] = 0
                    try:
                        eventData[atheData] = float(d[EventName])
                    except:
                        raise ValueError(
                            'Can not convert', d[EventName], 'to float. @Event='+EventName+',Athlete='+atheData)

            return eventData

    def GetSortingMethodByEventName(self, EventName):
        return False if KCMSConfig.MappedDataCompatiable(self.Config['General']['SortingMethods'])[EventName] == 'smallest' else True

    def GetPlacingToPoints(self):
        x = KCMSConfig.MappedDataCompatiable(
            self.Config['General']['PlacingToPoints'])
        r = {}
        for y in x:
            r[int(y)] = float(x[y])
        return r

    def SortDict(self, dic, reverse=False):
        if len(dic) == 0:
            return []

        keys = sorted(dic.keys(), key=lambda x: dic[x], reverse=reverse)
        ct = 0
        while dic[keys[0]] == 0 and ct < len(keys):
            ct += 1
            keys.append(keys.pop(0))
        return keys

    def SortEvent(self, EventName):
        EventResults = self.GetEventResultsByEventName(EventName)
        EventSort = self.SortDict(
            EventResults, self.GetSortingMethodByEventName(EventName))
        return EventSort

    def GetPointsByEventName(self, EventName):
        EventResults = self.GetEventResultsByEventName(EventName)
        EventSort = self.SortEvent(EventName)
        PlacingMap = self.GetPlacingToPoints()
        Ret = {}
        for x in range(len(EventSort)):
            if EventResults[EventSort[x]] == 0:
                Ret[EventSort[x]] = 0
            else:
                if x+1 in PlacingMap:
                    # Placing starts from 1
                    Ret[EventSort[x]] = PlacingMap[x+1]
                else:
                    Ret[EventSort[x]] = 0
        return Ret

    def GetPlacingByEventName(self, EventName):
        EventResults = self.GetEventResultsByEventName(EventName)
        EventSort = self.SortEvent(EventName)
        PlacingMap = self.GetPlacingToPoints()
        Ret = {}
        for x in range(len(EventSort)):
            Ret[EventSort[x]] = x + 1
        return Ret

    def GetAllAthletes(self):
        return self.Config['Data']['AthleteNames']

    def GetFinalPoints(self):
        Ret = {}
        for x in self.GetAllEvents():
            EventPoints = self.GetPointsByEventName(x)
            for y in self.GetAllAthletes():
                if y in EventPoints:
                    Ret[y] = Ret.get(y, 0) + EventPoints[y]

        return Ret

    def SaveConfig(self):
        return KCMSConfig.IO.Output.toConfig(self.Config)

    def GetAllEvents(self):
        return self.Config['General']['Events']

    def ExportFinalData(self, FileName='Export-Python.csv'):
        # Prepare data
        Columns = []
        FinalPoints = self.GetFinalPoints()

        # Generate Final Placing
        FinalPlacing = {}
        FinalPointsSorted = self.SortDict(FinalPoints, True)
        for x in range(len(FinalPointsSorted)):
            FinalPlacing[FinalPointsSorted[x]] = x+1

        Columns.append(('Overall Rank', FinalPlacing))
        Columns.append(('Overall Points', FinalPoints))
        for x in self.GetAllEvents():
            Columns.append(
                (x+' Result', self.GetEventResultsByEventName(x)))
            Columns.append((x+' Placing', self.GetPlacingByEventName(x)))
            Columns.append((x+' Point', self.GetPointsByEventName(x)))

        with open(FileName, 'w') as f:
            f.write(self.ConvertToCSV(Columns, FinalPointsSorted))

    def ConvertToCSV(self, Columns, Keys):
        f = 'Exported Data from KCMS (Python)\n'

        # Write headings
        for x in Columns:
            f += ','+x[0]
        f += '\n'

        # Write Data
        for x in Keys:
            f += x
            for y in Columns:
                f += ','+str(y[1][x])
            f += '\n'

        return f

# a=KCMS()
# a.ExportFinalData()
# print(a.GetPointsByEventName('1500m'))

# print(a.GetPlacingToPoints())
# print(a.GetEventResultsByEventName("1500m"))
# print(a.GetSortingMethodByEventName('1500m'))
# print(a.SortDict(a.GetEventResultsByEventName("1500m")))
