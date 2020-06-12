<p align="center">
  <a href="https://github.com/JayaFM/Jaya/" target="_blank">
    <img src="https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/Logo.png" alt="JayaFM Logo" >
  </a>
</p>
<h3 align="center">Jaya File Manager (JayaFM)</h3>
<p align="center">
  <img alt="Build status" src="https://github.com/JayaFM/Jaya/workflows/build/badge.svg">
  <a href="https://gitter.im/JayaCrossPlat/Jaya?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge" target="_blank"><img alt="Gitter Chat" src="https://badges.gitter.im/JayaCrossPlat/Jaya.svg"></a>
  <a href="https://github.com/JayaFM/Jaya/stargazers" target="_blank"><img alt="GitHub stars" src="https://img.shields.io/github/stars/JayaFM/Jaya"></a>
  <a href="https://github.com/JayaFM/Jaya/network" target="_blank"><img alt="GitHub forks" src="https://img.shields.io/github/forks/JayaFM/Jaya"></a>
  <a href="https://raw.githubusercontent.com/JayaFM/Jaya/dev/LICENSE" target="_blank"><img alt="MIT license" src="https://img.shields.io/github/license/JayaFM/Jaya"></a>
  <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=DEXCFJ6R48SR2" target="_blank"><img alt="Donate" src="https://img.shields.io/badge/Donate-PayPal-green.svg"></a>
</p>

## Table Of Contents

* [About The Project](#about-the-project)
  * [Built Using](#built-using)
  * [Screenshots](#screenshots)
* [Roadmap](#roadmap)
* [Contributing](#contributing)
* [Getting Started (Technical)](#getting-started)
* [License](#license)

## About The Project

**Jaya File Manager** is a .NET Core based cross platform file manager application which runs on Windows, Mac and Linux. Its goal is very simple, *"Allow browsing and managing of several file systems simultaneously using a single application which should work and look alike on all desktop platforms it supports."*.

Application is designed to be plug-able from the ground up i.e. anyone with experience of working with .NET Core will be able to add support for any new storage service by implementing a simple plugin. Support for below mentioned storage services are complete (or planned) as of now. If you would like addition of more storage services, please raise request [here](https://github.com/JayaFM/Jaya/issues).
- [x] File System
- [x] Dropbox
- [x] Google Drive
- [ ] Apple iCloud Drive ([help wanted](https://github.com/JayaFM/Jaya/issues/17))
- [ ] Microsoft OneDrive
- [ ] Box
- [ ] IDrive
- [ ] SugarSync
- [ ] pCloud
- [ ] Media Fire
- [x] FTP/SFTP
- [ ] Amazon S3
- [ ] Azure Blob Storage

This project is in early beta at the moment so it's not suitable for general use.

### Built Using

This project would have not existed without the availbility of below mentioned fantastic frameworks and tools.

* [.NET Core](https://github.com/dotnet/core)
* [Avalonia UI](https://avaloniaui.net/)
* [Visual Studio](https://visualstudio.microsoft.com/vs/)
* [Cake](https://cakebuild.net/)
* [Inno Setup](https://www.jrsoftware.org/isinfo.php)

### Screenshots

Dark Theme

![Dark Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/00.png)

![Dark Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/06.png)

![Dark Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/02.png)

![Dark Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/05.png)

Light Theme

![Light Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/01.png)

![Light Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/07.png)

![Light Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/03.png)

![Light Theme](https://raw.githubusercontent.com/JayaFM/Jaya/dev/docs/04.png)

## Roadmap

See the [open issues](https://github.com/JayaFM/Jaya/issues) and [project boards](https://github.com/JayaFM/Jaya/projects) for list of proposed features (and known issues). You are more than welcome to make feature requests and lodge any bugs you encounter. 

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are greatly appreciated.

If you are a .NET Core developer then you can develop plugins to support additional storage services or squash any existing bugs. You may also add new features or work on the ones which are not yet implemented.

A designer! Not a problem, create new vector icons and images for the UI. Suggestions and improvements in UX are also welcome.

None of the above, you can still buy the core contributors coffee or donate funds to procure Mac for development [here](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=DEXCFJ6R48SR2).

* [Rubal Walia](https://github.com/waliarubal): Original Concept and Development
* [Splitwirez](https://github.com/Splitwirez): UI and UX Specialist
* [Giorgio Zoppi](https://github.com/giorgiozoppi): Development

## Getting Started (Technical)

Technical users with programming experience can try things out, just open the project in Visual Studio 2017 IDE or Visual Studio 2019 IDE and run it in 'Debug' mode. Follow below mentioned steps to show your love and support towards project growth.

1. Fork the Project.
2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a pull request.

## License

Distributed under The 3-Clause BSD License. See [here](https://raw.githubusercontent.com/JayaFM/Jaya/dev/LICENSE) for more information.

<blockquote>
<p lang="en" dir="ltr">This is an amazing and clever thing by <a href="https://twitter.com/walia_rubal?ref_src=twsrc%5Etfw" target="_blank">@walia_rubal</a> - it&#39;s a *cross platform file explorer application* for Windows, Mac and Linux written in .NET Core/C# and Avalonia! <a href="https://github.com/JayaFM/Jaya">https://github.com/JayaFM/Jaya</a> SWEET. It&#39;s fun to see how people build things like this.</p>
&mdash; Scott Hanselman (@shanselman) <a href="https://twitter.com/shanselman/status/1186681229480906753?ref_src=twsrc%5Etfw" target="_blank">October 22, 2019</a>
</blockquote>
