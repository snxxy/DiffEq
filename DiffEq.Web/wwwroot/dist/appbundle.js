System.register("Components/NavButtonBarComponent", ["vue"], function (exports_1, context_1) {
    "use strict";
    var vue_1;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (vue_1_1) {
                vue_1 = vue_1_1;
            }
        ],
        execute: function () {
            exports_1("default", vue_1.defineComponent({
                props: {},
                emits: {
                    buttonPressEmit: null,
                },
                setup(props, { emit }) {
                    const buttonPressed = (pressedCode) => {
                        emit('buttonPressEmit', pressedCode);
                    };
                    return {
                        buttonPressed
                    };
                },
                template: "#nav-button-bar-template"
            }));
        }
    };
});
System.register("EquationCountModule", ["axios", "vue"], function (exports_2, context_2) {
    "use strict";
    var axios_1, vue_2, equationCount;
    var __moduleName = context_2 && context_2.id;
    function useGetEquationCount() {
        function getEquationCount() {
            axios_1.default.get('https://localhost:5001/api/homeapi/getequationcount')
                .then(response => {
                equationCount.equationCountByType = response.data;
                getTotalEquations();
            });
        }
        function getTotalEquations() {
            equationCount.equationTotal = 0;
            for (var i = 0, n = equationCount.equationCountByType.length; i < n; i++) {
                equationCount.equationTotal += equationCount.equationCountByType[i];
            }
        }
        return {
            equationCount,
            getEquationCount
        };
    }
    exports_2("useGetEquationCount", useGetEquationCount);
    return {
        setters: [
            function (axios_1_1) {
                axios_1 = axios_1_1;
            },
            function (vue_2_1) {
                vue_2 = vue_2_1;
            }
        ],
        execute: function () {
            equationCount = vue_2.reactive({
                equationCountByType: [],
                equationTotal: 0
            });
        }
    };
});
System.register("Components/TestComponent", ["axios", "MathJax", "vue"], function (exports_3, context_3) {
    "use strict";
    var axios_2, MathJax_1, vue_3, vue_4;
    var __moduleName = context_3 && context_3.id;
    return {
        setters: [
            function (axios_2_1) {
                axios_2 = axios_2_1;
            },
            function (MathJax_1_1) {
                MathJax_1 = MathJax_1_1;
            },
            function (vue_3_1) {
                vue_3 = vue_3_1;
                vue_4 = vue_3_1;
            }
        ],
        execute: function () {
            exports_3("default", vue_4.defineComponent({
                props: {
                    isenabled: {
                        type: Boolean,
                        required: true,
                        default: false
                    },
                    typeonecount: {
                        type: Number,
                        required: false,
                        default: 2
                    },
                    typetwocount: {
                        type: Number,
                        required: false,
                        default: 2
                    }
                },
                emits: {},
                setup(props, { emit }) {
                    const equations = vue_3.ref([]);
                    const userSolutions = vue_3.ref([]);
                    const checkerObject = vue_3.ref([]);
                    const solutionResults = vue_3.ref([]);
                    const solutionsChecked = vue_3.ref(false);
                    const equationsTotal = vue_3.ref(0);
                    const userScore = vue_3.ref(0);
                    function refreshJax() {
                        MathJax_1.default.typesetClear();
                        MathJax_1.default.typeset();
                    }
                    function sendGetEqOrder(typeOneCount, typeTwoCount) {
                        axios_2.default({
                            method: "POST",
                            url: 'https://localhost:5001/api/homeapi/getequationstosolve',
                            data: {
                                "sveq": typeOneCount,
                                "hgeq": typeTwoCount
                            }
                        })
                            .then(response => {
                            equations.value = response.data;
                            console.log(response.data);
                            equationsTotal.value = equations.value.length;
                            setTimeout(() => refreshJax(), 500);
                        })
                            .catch(function (error) {
                            console.log(error);
                        });
                    }
                    function fillCheckerObject() {
                        for (var i = 0; i < equationsTotal.value; i++) {
                            checkerObject.value.push({
                                userSolution: userSolutions.value[i],
                                realSolution: equations.value[i].solution
                            });
                        }
                    }
                    function sendSolutionsForCheck() {
                        console.log("sending check");
                        console.log(checkerObject.value);
                        axios_2.default({
                            method: "POST",
                            url: "https://localhost:5001/api/homeapi/checksolutions",
                            data: {
                                "solutions": checkerObject.value
                            }
                        })
                            .then(response => {
                            console.log("got response");
                            console.log(response.data);
                            solutionResults.value = response.data;
                        })
                            .catch(error => {
                            console.log(error);
                        });
                    }
                    function checkButtonPress() {
                        userScore.value = 0;
                        fillCheckerObject();
                        sendSolutionsForCheck();
                        for (var i = 0; i < solutionResults.value.length; i++) {
                            if (solutionResults[i].value == true) {
                                userScore.value++;
                            }
                        }
                        solutionsChecked.value = true;
                    }
                    function resetControlsState() {
                        solutionsChecked.value = false;
                        userScore.value = 0;
                        while (userSolutions.value.length > 0) {
                            userSolutions.value.pop();
                        }
                        while (solutionResults.value.length > 0) {
                            solutionResults.value.pop();
                        }
                        while (checkerObject.value.length > 0) {
                            checkerObject.value.pop();
                        }
                    }
                    function isPressedWatcherCheck() {
                        resetControlsState();
                        if (props.isenabled) {
                            sendGetEqOrder(props.typeonecount, props.typetwocount);
                        }
                    }
                    vue_3.watch(() => props.isenabled, isPressedWatcherCheck);
                    return {
                        refreshJax,
                        sendGetEqOrder,
                        equations,
                        checkButtonPress,
                        solutionResults,
                        userScore,
                        solutionsChecked,
                        equationsTotal,
                        userSolutions
                    };
                },
                template: "#test-template"
            }));
        }
    };
});
System.register("Components/CustomTestComponent", ["vue", "EquationCountModule", "Components/TestComponent"], function (exports_4, context_4) {
    "use strict";
    var vue_5, EquationCountModule_1, TestComponent_1;
    var __moduleName = context_4 && context_4.id;
    return {
        setters: [
            function (vue_5_1) {
                vue_5 = vue_5_1;
            },
            function (EquationCountModule_1_1) {
                EquationCountModule_1 = EquationCountModule_1_1;
            },
            function (TestComponent_1_1) {
                TestComponent_1 = TestComponent_1_1;
            }
        ],
        execute: function () {
            exports_4("default", vue_5.defineComponent({
                props: {
                    ispressed: {
                        type: Boolean,
                        required: true,
                        default: false
                    }
                },
                emits: {},
                setup(props, { emit }) {
                    const sveq = vue_5.ref(0);
                    const hgeq = vue_5.ref(0);
                    const errors = vue_5.ref([]);
                    const isTestStarted = vue_5.ref(false);
                    const isSetupEnabled = vue_5.ref(false);
                    const { equationCount, getEquationCount } = EquationCountModule_1.useGetEquationCount();
                    vue_5.onMounted(getEquationCount);
                    function validateAndSend() {
                        while (errors.value.length > 0) {
                            errors.value.pop();
                        }
                        if (sveq.value + hgeq.value == 0 || sveq.value + hgeq.value > 10) {
                            errors.value.push("Equaton total count must be between 0 and 10");
                        }
                        if (!errors.value.length) {
                            isTestStarted.value = true;
                        }
                    }
                    function isPressedSwitchWatcher() {
                        if (props.ispressed) {
                            isTestStarted.value = false;
                            isSetupEnabled.value = true;
                        }
                        else {
                            isSetupEnabled.value = false;
                            isTestStarted.value = false;
                        }
                    }
                    vue_5.watch(() => props.ispressed, isPressedSwitchWatcher);
                    return Object.assign({ sveq,
                        hgeq,
                        errors,
                        validateAndSend,
                        isTestStarted,
                        isSetupEnabled }, vue_5.toRefs(equationCount));
                },
                components: {
                    TestComponent: TestComponent_1.default
                },
                template: "#custom-test-template"
            }));
        }
    };
});
System.register("Components/GenerationComponent", ["axios", "vue", "EquationCountModule"], function (exports_5, context_5) {
    "use strict";
    var axios_3, vue_6, EquationCountModule_2;
    var __moduleName = context_5 && context_5.id;
    return {
        setters: [
            function (axios_3_1) {
                axios_3 = axios_3_1;
            },
            function (vue_6_1) {
                vue_6 = vue_6_1;
            },
            function (EquationCountModule_2_1) {
                EquationCountModule_2 = EquationCountModule_2_1;
            }
        ],
        execute: function () {
            exports_5("default", vue_6.defineComponent({
                props: {
                    ispressed: {
                        type: Boolean,
                        required: true,
                        default: false
                    }
                },
                emits: {},
                setup(props, { emit }) {
                    const sveq = vue_6.ref(0);
                    const hgeq = vue_6.ref(0);
                    const errors = vue_6.ref([]);
                    const { equationCount, getEquationCount } = EquationCountModule_2.useGetEquationCount();
                    vue_6.onMounted(getEquationCount);
                    function validateAndSend() {
                        while (errors.value.length > 0) {
                            errors.value.pop();
                        }
                        if (sveq.value + hgeq.value == 0) {
                            errors.value.push("Equaton total count must be greater than 0");
                        }
                        if (!errors.value.length) {
                            sendGenOrder();
                        }
                    }
                    function sendGenOrder() {
                        axios_3.default({
                            method: 'POST',
                            url: 'https://localhost:5001/api/homeapi/generateequationorder',
                            data: {
                                "sveq": sveq.value,
                                "hgeq": hgeq.value
                            }
                        })
                            .then(response => {
                            console.log(response);
                            getEquationCount();
                        })
                            .catch(error => {
                            console.log(error);
                        });
                    }
                    return Object.assign({ sveq,
                        hgeq,
                        errors,
                        validateAndSend }, vue_6.toRefs(equationCount));
                },
                template: "#generation-template"
            }));
        }
    };
});
System.register("Components/RandomTestComponent", ["vue", "Components/TestComponent"], function (exports_6, context_6) {
    "use strict";
    var vue_7, TestComponent_2;
    var __moduleName = context_6 && context_6.id;
    return {
        setters: [
            function (vue_7_1) {
                vue_7 = vue_7_1;
            },
            function (TestComponent_2_1) {
                TestComponent_2 = TestComponent_2_1;
            }
        ],
        execute: function () {
            exports_6("default", vue_7.defineComponent({
                props: {
                    ispressed: {
                        type: Boolean,
                        required: true,
                        default: false
                    }
                },
                emits: {},
                setup(props, { emit }) {
                    const isRandomTestEnabled = vue_7.ref(false);
                    function isPressedWatcherCheck() {
                        isRandomTestEnabled.value = props.ispressed;
                    }
                    vue_7.watch(() => props.ispressed, isPressedWatcherCheck);
                    return {
                        isRandomTestEnabled
                    };
                },
                components: {
                    TestComponent: TestComponent_2.default,
                },
                template: "#random-test-template"
            }));
        }
    };
});
System.register("Components/DefaultIndexComponent", ["vue"], function (exports_7, context_7) {
    "use strict";
    var vue_8;
    var __moduleName = context_7 && context_7.id;
    return {
        setters: [
            function (vue_8_1) {
                vue_8 = vue_8_1;
            }
        ],
        execute: function () {
            exports_7("default", vue_8.defineComponent({
                props: {
                    isenabled: {
                        type: Boolean,
                        required: true,
                        default: true
                    }
                },
                emits: {},
                setup(props, { emit }) {
                    return {};
                },
                template: "#default-index-template"
            }));
        }
    };
});
System.register("App", ["vue", "Components/NavButtonBarComponent", "Components/CustomTestComponent", "Components/GenerationComponent", "Components/RandomTestComponent", "Components/DefaultIndexComponent"], function (exports_8, context_8) {
    "use strict";
    var vue_9, NavButtonBarComponent_1, CustomTestComponent_1, GenerationComponent_1, RandomTestComponent_1, DefaultIndexComponent_1;
    var __moduleName = context_8 && context_8.id;
    return {
        setters: [
            function (vue_9_1) {
                vue_9 = vue_9_1;
            },
            function (NavButtonBarComponent_1_1) {
                NavButtonBarComponent_1 = NavButtonBarComponent_1_1;
            },
            function (CustomTestComponent_1_1) {
                CustomTestComponent_1 = CustomTestComponent_1_1;
            },
            function (GenerationComponent_1_1) {
                GenerationComponent_1 = GenerationComponent_1_1;
            },
            function (RandomTestComponent_1_1) {
                RandomTestComponent_1 = RandomTestComponent_1_1;
            },
            function (DefaultIndexComponent_1_1) {
                DefaultIndexComponent_1 = DefaultIndexComponent_1_1;
            }
        ],
        execute: function () {
            exports_8("default", vue_9.defineComponent({
                setup() {
                    const buttonPressedCode = vue_9.ref(0);
                    function buttonPressed(pressCode) {
                        if (buttonPressedCode.value == pressCode) {
                            buttonPressedCode.value = 0;
                        }
                        else {
                            buttonPressedCode.value = pressCode;
                        }
                    }
                    return {
                        buttonPressed,
                        buttonPressedCode
                    };
                },
                components: {
                    NavButtonBarComponent: NavButtonBarComponent_1.default,
                    RandomTestComponent: RandomTestComponent_1.default,
                    CustomTestComponent: CustomTestComponent_1.default,
                    GenerationComponent: GenerationComponent_1.default,
                    DefaultIndexComponent: DefaultIndexComponent_1.default
                },
                template: "#app-index-template"
            }));
        }
    };
});
System.register("main", ["vue", "App"], function (exports_9, context_9) {
    "use strict";
    var vue_10, App_1;
    var __moduleName = context_9 && context_9.id;
    return {
        setters: [
            function (vue_10_1) {
                vue_10 = vue_10_1;
            },
            function (App_1_1) {
                App_1 = App_1_1;
            }
        ],
        execute: function () {
            vue_10.createApp(App_1.default).mount('#app');
        }
    };
});
//# sourceMappingURL=appBundle.js.map