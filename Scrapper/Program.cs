using Scrapper;
using System;

var ajansPress = new AjansPressScrapper();
var ajans1 = ajansPress.Scrap("https://gold.ajanspress.com.tr/linki/VZ96VSygUbU-XhsRS7y8nQ2/?v=2&s=10851&b=468796&isH=1");
// TimeSpan BroadcastTime
var ajans2 = ajansPress.Scrap("https://gold.ajanspress.com.tr/popuptv/RouVvgeNBCprQBhzXJsmFQ2/?v=2&s=7341&b=346802&isH=1");
var ajans3 = ajansPress.Scrap("https://gold.ajanspress.com.tr/linkpress/tokeWXgF9gROOOt_PMDR-A2/?v=2&s=58&b=125495&isH=1");

var medyaTakip = new MedyaTakipScrapper();
var medya1 = medyaTakip.Scrap("https://clips.medyatakip.com/dm/clip/mP9YFGtbKbqs6RaLb5vnjK");
var medya2 = medyaTakip.Scrap("https://clips.medyatakip.com/pm/clip/BVb5hQtd1dOHRJbjQ8JMeW#2022110004260131");

var interPress = new InterPressScrapper();
var interPress1 = interPress.Scrap("http://web.interpress.com/app/document/viewer/7bb53910-e0d4-4b3f-a5d5-97e825048608?cid=AuB7GkISpOI%3D");
var interPress2 = interPress.Scrap("https://web.interpress.com/app/document/viewer/63b49557-d719-4c80-ae81-950bb1449c32?cid=AuB7GkISpOI%3D");
var interPress3 = interPress.Scrap("http://web.interpress.com/app/document/viewer/3acb9dd3-56a0-480a-9f3b-3995c25bdcff?cid=%2BPVQol%2BuiPM%3D");


Console.ReadLine();
