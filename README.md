# wam-plugin-sample

WAM 플러그인 **레퍼런스 예제이자 템플릿**. 새 플러그인을 만들 때 이 레포를 복사해서 시작하세요.
모든 확장 지점을 보여줍니다: 상태 집합을 가진 노드 타입, 관계 타입, 스키마 기반 설정 페이지.

- **PluginId**: `wam.plugin.sample`
- 의존성: [Jaywapp.Wam.Core](https://www.nuget.org/packages/Jaywapp.Wam.Core) 하나뿐

## 플러그인 만드는 법 (요약)

1. `Jaywapp.Wam.Core` 패키지를 참조하고 `IWamPlugin`을 구현한다
   (`GetNodeTypes` / `GetRelationTypes` / `GetSettingsPages`).
2. 설정은 `PluginSettingsPage` + `PluginSettingField`로 **선언**한다 — WPF 불필요.
   호스트가 폼을 자동 렌더링하고, 값을 필드 `Key`로 저장한다.
3. 런타임에 설정 값을 읽을 땐 `IPluginSettingsStore.Load(pluginId)`의
   `GetString/GetInt/GetBool`을 쓴다 (예: `SampleSettings.From`).
4. `build/plugin.json` 매니페스트를 함께 배포한다.

## 빌드 & 설치

```bash
dotnet build -c Release
```

산출물 `bin/Release/net8.0/Wam.Plugins.Sample.dll` 과 `build/plugin.json` 을
`%AppData%\WAM\plugins\wam.plugin.sample\` 에 복사한 뒤 WAM을 재시작한다.

## 구조

```
wam-plugin-sample/
├── README.md
├── nuget.config              # 개발용 로컬 Jaywapp.Wam.Core 피드
├── build/plugin.json         # 매니페스트
└── src/Wam.Plugins.Sample/
    ├── Wam.Plugins.Sample.csproj
    └── SamplePlugin.cs        # IWamPlugin + 설정 스키마 + 값 매핑
```
