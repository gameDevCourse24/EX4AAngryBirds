<div dir='rtl' lang='he'>

# גרסה של Angry Birds
## 🎮 תיאור המשחק
זהו שיבוט של המשחק Angry Birds שנבנה ב-Unity.  
במשחק, השחקנים משגרים ציפורים כדי להשמיד חזירים באמצעות מכניקת משיכה ושחרור.

**[קישור למשחק](https://gamedevrel2024shovhalyon.itch.io/angrybirdsminiversion)**

## 📋 איך לשחק
1. משוך את הציפור לאחור באמצעות העכבר.
2. כוון לעבר החזירים.
3. שחרר כדי לשגר את הציפור.
4. נסה להרוס את כל החזירים עם מספר הציפורים המוגבל שניתן לך. **שים לב שפגיעת מבנה בחזיר לא הורג אותו, ולכן יש לשחק בחכמה ולהבין מתי ואיך לפגוע במבנה כדי שלא יכסה לך חזיר ואז לא תהיה לך אפשרות להרוג אותו**

## 🔧 מכניקת השלבים
- לכל שלב יש מספר מוגבל של ציפורים.
- החזירים נהרסים לאחר מספר פגיעות מסוים (תלוי בחוזק החזיר).
- המצלמה הראשית עוקבת אחרי הציפור בציר ה-X.
- הציפור נהרסת אוטומטית אחרי זמן מוגדר מרגע הפגיעה הראשונה בקרקע, חומה או חזיר.

## רכיבים מכניקות וטכניקות שהשתמשתי בהם
- רכיב קפיץ שמחובר לציפור ולוו - החיבור קורה בקוד ככה שבטעינת ציפור חדשה הכל עובד כמו שצריך.
- הקפיץ משתחרר כשעוזבים את הציפור, כדי שלא תישאר מחוברת ותוכל לעוף (כמו רוגטקה).
- אובייקט קרקע שניתן למשוך לגודל הרצוי, והוא מוסיף יחידות קרקע לפי הצורך (Sprite Shape).
- כל האובייקטים הם prefeb.
- הגדלת המצלמה בזמן משיכת הציפור.
- רכיב gameManager לניהול המשחק, וככה כל רכיבי הקוד האחרים קצרים מובנים וקריאים יותר.
- ספירה לאחור לפני השמדת ציפור, וככה יש לה זמן לפגוע בעוד דברים. (זה מאפשר חשיבה ושימוש בחוקי פיזיקה כדי להשמיד את החזירים מעבר לשימוש בפגיעה ישירה, ומצד שני נותן את האופציה שהציפור תושמד מתישהו ולא תטייל לעד).

## 💻 מבנה הקוד

### AngryBird.cs
מטפל במכניקת המשיכה והשחרור של הציפור:
- מאפשר משיכת הציפור עם מגבלת מרחק.
- מתאים את גודל המצלמה בזמן המשיכה.
- משחרר את הציפור מהקפיץ אחרי זמן מוגדר.

### GameManager.cs
מנהל את הלוגיקה הכללית של המשחק:
- מעקב אחר מספר הציפורים והחזירים.
- טעינת ציפורים חדשות.
- בדיקת תנאי ניצחון והפסד.
- ניהול מעבר בין שלבים.

### CameraFollower.cs
שולט בהתנהגות המצלמה:
- עוקב אחרי הציפור בציר ה-X.
- מאפשר שינוי גודל תצוגת המצלמה.
- בתחילת שלב- מבצע תנועת מצלמה חלקה להצגת השלב.

### Pig.cs
מגדיר את התנהגות החזירים:
- שליטה בחוזק החזיר מחלון הinspector.
- מעקב אחר מספר הפגיעות בחזיר.
- השמדת החזיר כשמגיע למספר הפגיעות המוגדר.

### Border.cs
מטפל בגבולות המשחק:
- הורס אובייקטים שיוצאים מגבולות המשחק.

## 🛠️ הגדרת שלב חדש בקלות
1. פתח את הפרויקט ב-Unity.
2. פתח את הסצינה LevelCreator.
3. במסך ההיררכיה לחץ על 3 הנקודות שליד שם הסצינה.
4. בחר באופציה "Save Scene As".
5. בחר את התיקייה הרצויה (Scenes) לשמירה של הסצינה.
6. בחר שם לשלב ולחץ על Save.
7. פתח את השלב החדש שיצרת.
8. עבור ל File -> build Prifile ->Scene List.
9. לחץ על הכפתור Add Open Scene, וסגור את החלון הזה.
10. בשלב האחד לפני האחרון (מה שהיה אחרון לפני יצרת את החדש הזה), תחת האובייקט gameManager תחת הרכיב GameManager במאפיין next Scene Name את השם של השלב החדש שהוספת כעת.
11. בשלב האחרון (השלב החדש שיצרת), תחת האובייקט gameManager תחת הרכיב GameManager במאפיין next Scene Name את השם YouWinLevel, כדי שבניצחון יועברו לשלב הזה.
12. כעת עצב את הסצינה שלך כרצונך.
13. נקודות חשובות לעיצוב סצינה:
    - תמקם את נקודות ההתחלה והסיום של המצלמה, כדי שבתחילת השלב היא תראה את כל החזירים והחומות. (לא את המצלמה עצמה, אלה אובייקטים ריקים בשם startPoint, EndPoint שנמצאים כבר בסצינה).
    - אל תשים ציפור בשלב, המשחק עושה את זה לבד בקוד.
    - ערוך את גודל המצלמה כרצונך - עבור שלבים קטנים באורכם אולי כדאי לדים מצלמה עם מספר נמוך יותר.


## 🎯 תכונות עיקריות
- מערכת פיזיקה מבוססת Rigidbody2D.
- מכניקת משיכה ושחרור אינטואיטיבית.
- מעקב מצלמה חלק.
- ניהול שלבים.
- פאנלים לניצחון והפסד.

## 🔄 בכל שלב
1. הצגת השלב עם תנועת מצלמה.
2. שיגור ציפורים.
3. בדיקת מצב המשחק אחרי כל ציפור.
4. טעינת ציפור אם מתאפשר או הצגת פאנל ניצחון\הפסד במקרה הצורך.
<\div>
