import { defineComponent, ref } from 'vue'

import NavButtonBarComponent from './Components/NavButtonBarComponent'
import CustomTestComponent from './Components/CustomTestComponent'
import GenerationComponent from './Components/GenerationComponent'
import RandomTestComponent from './Components/RandomTestComponent'
import DefaultIndexComponent from './Components/DefaultIndexComponent'

export default defineComponent({
    setup() {
        const buttonPressedCode = ref(0)

        function buttonPressed(pressCode) {
            if (buttonPressedCode.value == pressCode) {
                buttonPressedCode.value = 0
            }
            else {
                buttonPressedCode.value = pressCode
            }
        }

        return {
            buttonPressed,
            buttonPressedCode
        }
    },
    components: {
        NavButtonBarComponent,
        RandomTestComponent,
        CustomTestComponent,
        GenerationComponent,
        DefaultIndexComponent
    },
    template: "#app-index-template"
})