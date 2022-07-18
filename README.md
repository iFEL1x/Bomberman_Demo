# <p align="center"> Bomberman DEMO</p>

<div align="Center">
    <img src = https://github.com/iFEL1x/iFEL1x/blob/main/Resources/Screenshots/Screen(Bomberman)(0).png width="600">
</div>


## Описание проекта

Данный проект является изучением статьи 
***"How To Make A Game Like Bomberman With Unity"*** на сайте сообщества [Unity3DSchool.com](https://www.raywenderlich.com/)

Проект собран в Unity3D с использованием языка программирование C# и принципов ООП

___
## Скачивание и установка
Для того что бы запустить проект на своем ПК

* [Скачайте](https://unity3d.com/ru/get-unity/download) и [установите](https://docs.unity3d.com/2018.2/Documentation/Manual/InstallingUnity.html) Unity3D последней версии с официального сайта.
* Скачайте проект по [ссылке](https://github.com/iFEL1x/Platformer2D_Android_Demo_Level/archive/refs/heads/main.zip) или с текущей странице "Code\Download ZIP".
    + Распакуйте архив на своем ПК.
* Запустите Unity3D
    + Рядом с кнопкой "Open" нажмите на стрелочку :arrow_down_small:, в открывшимся списке выберете "Add project from disk"
    + Выберете путь распаковки проекта, нажмите "Add Project"

___
## В данном проекте применяется
* Массивы, списки, очереди с циклами.
* Создание цепочек событий через коллизии.
* Построение всего проекта максимально подводилось под ООП.

*Демонсрация кода:*

```C#
    private void DropBomb()
    {
        if (_bombPool.Count > 0)
        {
            Bomb bombTemp = _bombPool[0];
            _bombPool.Remove(bombTemp);
            bombTemp.gameObject.SetActive(true);
            bombTemp.transform.parent = null;
            bombTemp.transform.position =
                new Vector3(Mathf.RoundToInt(transform.position.x),
                bombPrefab.transform.position.y,
                Mathf.RoundToInt(transform.position.z));
        }
    }
```

**Основная задача проекта** - Изучение возможностей Unity3D и языка программирования С# и принципов ООП.

*Демонстрация финальной части игрового процесса:*

![Bomberman](https://github.com/iFEL1x/iFEL1x/blob/main/Resources/Image/Gif/mp4%20to%20GIH(Bomberman).gif)
